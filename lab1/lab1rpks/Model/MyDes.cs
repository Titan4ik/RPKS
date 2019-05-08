using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace lab1rpks.Model.Interfaces.Implementations
{
    public class MyDes
    {
       

        private const int quantityOfRounds = 16; //количество раундов

       

        private UInt64 _block;

        private const int sizeBlock= 130712;
      
        private int _currentProgress;
        private BackgroundWorker _worker;
        private Action<string> _onPropertyChanged;

        public MyDes(ref int currentProgress,
            BackgroundWorker worker, Action<string> onPropertyChanged)
        {
            _currentProgress = currentProgress;
            _worker = worker;
            _onPropertyChanged = onPropertyChanged;
            KeyBites = _keyBytes;
        }

        private byte[] _keyBytes = new byte[] { 12, 1, 31, 214, 124, 21, 1 };
        private UInt32 _keyEncode;
        private UInt32 _keyDecode;

        public byte[] KeyBites
        {
            get
            {

                return _keyBytes;
            }
            set
            {
                _keyBytes = value;

                _keyEncode = BitConverter.ToUInt32(CorrectKeyWord(_keyBytes), 0);
                _keyDecode = BitConverter.ToUInt32(CorrectKeyWord(_keyBytes), 0);
                for (int j = 0; j < quantityOfRounds - 1; j++)
                {



                    _keyDecode = KeyToNextRound(_keyDecode);
                }



            }
        }

        //доводим строку до размера, чтобы делилась на sizeOfBlock
        private byte[] StringToRightLength(byte[] input)
        {
            byte[] res;
            if (input.Length == sizeBlock)
                res = input;
            else
            {
                res=new byte[sizeBlock];
                for (int i = 0; i < input.Length; i++)
                {
                    res[i] = input[i];
                }
                for (int i = input.Length; i < sizeBlock; i++)
                {
                    res[i] =0;
                }
            }
                

            return res;
        }

      

        //доводим длину ключа до нужной
        private byte[] CorrectKeyWord(byte[] input, int lengthKey=4)
        {
            byte[] res=null;
            if (input.Length == lengthKey)
                res = input;
            else if (input.Length > lengthKey)
            {
                res=new byte[lengthKey];
                for (int i = 0; i < lengthKey; i++)
                {
                    res[i] = input[i];
                }
            }
            else if (input.Length < lengthKey)
            {
                res = new byte[lengthKey];
                for (int i = 0; i < input.Length; i++)
                {
                    res[i] = input[i];
                }
                for (int i = input.Length; i < lengthKey; i++)
                {
                    res[i] = 0;
                }
            }


            return res;
        }

        //шифрование DES один раунд
        private UInt64 EncodeDES_One_Round(UInt64 input, UInt32 key)
        {
           
           

            UInt64 L = input >> 32;
            UInt64 R = (input << 32)>>32;


            UInt64 result = R<<32;

            UInt64 resultTemp = XOR(L, f(R, (UInt64)key));
            result += resultTemp;



            return result;
        }
    

        //расшифровка DES один раунд
        private UInt64 DecodeDES_One_Round(UInt64 input, UInt32 key)
        {
            UInt64 L = input >> 32;
            UInt64 R = (input << 32) >> 32;
            UInt64 result = L;
            UInt64 resultTemp = XOR(L, f(R, key))<<32;
            result += resultTemp;
           
            return result;
        }
        //XOR двух строк с двоичными данными
        private UInt64 XOR(UInt64 s1, UInt64 s2)
        {           
            return s1^s2;
        }

        //шифрующая функция f. в данном случае это XOR
        private UInt64 f(UInt64 s1, UInt64 s2)
        {
            return XOR(s1, s2);
        }

        //вычисление ключа для следующего раунда шифрования. циклический сдвиг >> 2
        private UInt32 KeyToNextRound(UInt32 key)
        {
            Model2 model2= new Model2();


            return model2.CyclicShiftRight(key, 2);
        }

        //вычисление ключа для следующего раунда расшифровки. циклический сдвиг << 2
        private UInt32 KeyToPrevRound(UInt32 key)
        {
            Model2 model2 = new Model2();


            return model2.CyclicShiftLeft(key, 2);
        }






        ///
        ///without
        //сделать апдейт до 1024
        public string Encrypt( string inputFileName, string outputFileName)
        {
            _currentProgress = 0;
            double progress = 0;
            double oneTick = 0;


            try
            {
                using (BinaryReader fsread = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {
                        
                        long length = new System.IO.FileInfo(inputFileName).Length;
                        if (length == 0) throw new Exception("Пустой файл");


                        var count = length / sizeBlock;
                        if (count * sizeBlock < length) count++;
                        oneTick = 100 / (double)(count* (sizeBlock/8));

                        for (int i = 0; i < count; i++)
                        {

                            byte[] readArr = new byte[sizeBlock];


                            readArr = fsread.ReadBytes(sizeBlock);
                            var readTemp = StringToRightLength(readArr);

                            UInt64[] result = new ulong[sizeBlock / 8];
                            for (int q = 0; q < (sizeBlock / 8); q++)
                            {
                                _block = BitConverter.ToUInt64(readTemp, q * 8);
                                UInt32 key = _keyEncode;
                                for (int j = 0; j < quantityOfRounds; j++)
                                {
                                    _block = EncodeDES_One_Round(_block, key);
                                    key = KeyToNextRound(key);

                                }
                                result[q] = _block;


                                progress += oneTick;
                                if (_currentProgress != (int)progress)
                                {
                                    _currentProgress = (int)progress;
                                    _worker.ReportProgress(_currentProgress);

                                    Thread.Sleep(1);
                                    _onPropertyChanged("CurrentProgress");
                                }
                            }

                            byte[] resultBytes = new byte[result.Length * 8];

                            for (int j = 0; j < result.Length; j++)
                            {
                                var temp = BitConverter.GetBytes(result[j]);
                                for (int k = 0; k < 8; k++)
                                {
                                    resultBytes[k + 8 * j] = temp[k];
                                }
                            }
                            fswrite.Write(resultBytes, 0, resultBytes.Length);

                        }
                    }
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";

          

        }

        public string EncryptNoProgressDisplay(string inputFileName, string outputFileName)
        {
            try
            {
                using (BinaryReader fsread = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {
                        long length = new System.IO.FileInfo(inputFileName).Length;
                        if (length == 0) throw new Exception("Пустой файл");


                        var count = length / sizeBlock;
                        if (count * sizeBlock < length) count++;


                        for (int i = 0; i < count; i++)
                        {

                            byte[] readArr = new byte[sizeBlock];


                            readArr = fsread.ReadBytes(sizeBlock);
                            var readTemp = StringToRightLength(readArr);

                        
                            for (int q = 0; q < (sizeBlock/8); q++)
                            {
                                _block = BitConverter.ToUInt64(readTemp, q*8);
                                UInt32 key = _keyEncode;
                                for (int j = 0; j < quantityOfRounds; j++)
                                {
                                    _block = EncodeDES_One_Round(_block, key);
                                    key = KeyToNextRound(key);

                                }
                              
                                fswrite.Write(BitConverter.GetBytes(_block), 0, 8);

                            }

                           

                        }
                    }
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";

        }

        public string DecipherNoProgressDisplay(string inputFileName, string outputFileName)
        {

            try
            {

                using (BinaryReader fsread = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {

                        long length = new System.IO.FileInfo(inputFileName).Length;
                        if (length == 0) throw new Exception("Пустой файл");


                        var count = length / sizeBlock;
                        if (count * sizeBlock < length) count++;

                        for (int i = 0; i < count; i++)
                        {

                            byte[] readArr = new byte[sizeBlock];


                            readArr = fsread.ReadBytes(sizeBlock);
                            var readTemp = StringToRightLength(readArr);

                            UInt64[] result=new ulong[sizeBlock / 8];
                            for (int q = 0; q < (sizeBlock / 8); q++)
                            {
                                _block = BitConverter.ToUInt64(readTemp, q * 8);
                                UInt32 key = _keyDecode;


                                for (int j = 0; j < quantityOfRounds; j++)
                                {
                                    _block = DecodeDES_One_Round(_block, key);
                                    key = KeyToPrevRound(key);
                                }

                                if (i != (count - 1))
                                    fswrite.Write(BitConverter.GetBytes(_block), 0, 8);
                                else
                                {
                                    result[q] = _block;
                                }

                            }


                           // byte[] resultBytes=new byte[result.Length*8];

                           
                            
                            if (i == count - 1)
                            {
                                int j;
                                for ( j= result.Length - 1; j >= 0; j--)
                                {
                                    if (result[j] != 0) break;
                                }

                                for (int k = 0; k <= j; k++)
                                {
                                    var temp = BitConverter.GetBytes(result[k]);
                                    fswrite.Write(temp, 0, 8);
                                }
                            }
                                        
                           
                     
                        }
                    }
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";

        }
        //расшифровать
    
        public string Decipher(string inputFileName, string outputFileName)
        {
            _currentProgress = 0;
            double progress = 0;
            double oneTick = 0;

            try
            {

                using (BinaryReader fsread = new BinaryReader(File.Open(inputFileName, FileMode.Open)))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {

                        long length = new System.IO.FileInfo(inputFileName).Length;
                        if (length == 0) throw new Exception("Пустой файл");


                        var count = length / sizeBlock;
                        if (count * sizeBlock < length) count++;
                        oneTick = 100 / (double)(count * (sizeBlock / 8));

                        for (int i = 0; i < count; i++)
                        {

                            byte[] readArr = new byte[sizeBlock];


                            readArr = fsread.ReadBytes(sizeBlock);
                            var readTemp = StringToRightLength(readArr);

                            UInt64[] result = new ulong[sizeBlock / 8];
                            for (int q = 0; q < (sizeBlock / 8); q++)
                            {
                                _block = BitConverter.ToUInt64(readTemp, q * 8);
                                UInt32 key = _keyDecode;


                                for (int j = 0; j < quantityOfRounds; j++)
                                {
                                    _block = DecodeDES_One_Round(_block, key);
                                    key = KeyToPrevRound(key);
                                }

                                result[q] = _block;

                                progress += oneTick;
                                if (_currentProgress != (int)progress)
                                {
                                    _currentProgress = (int)progress;
                                    _worker.ReportProgress(_currentProgress);

                                    Thread.Sleep(1);
                                    _onPropertyChanged("CurrentProgress");
                                }

                            }


                            byte[] resultBytes = new byte[result.Length * 8];

                            for (int j = 0; j < result.Length; j++)
                            {

                                var temp = BitConverter.GetBytes(result[j]);
                                for (int k = 0; k < 8; k++)
                                {
                                    resultBytes[k + 8 * j] = temp[k];
                                }
                            }

                            int countnullchar = 0;
                            if (i == count - 1)
                            {
                                for (int j = result.Length - 1; j >= 0; j--)
                                {
                                    if (result[j] == 0) countnullchar++;
                                    else
                                        break;
                                }
                            }

                            fswrite.Write(resultBytes, 0, resultBytes.Length - (countnullchar * 8));

                        }
                    }
                }

            }
            catch (Exception e)
            {
                return e.Message;
            }

            return "";
           

        }

      

    }
}
