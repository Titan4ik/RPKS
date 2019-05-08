using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;


namespace lab1rpks.Model.Interfaces.Implementations
{
    public class EncryptionAlgorithm : IEncryptionAlgorithm
    {
        private  int _currentProgress;
        private BackgroundWorker _worker;
        private Action<string> _onPropertyChanged;

        public EncryptionAlgorithm(ref int currentProgress,
            BackgroundWorker worker, Action<string> onPropertyChanged)
        {
            _currentProgress = currentProgress;
            _worker = worker;
            _onPropertyChanged = onPropertyChanged;
        }

        public string MyAlgorithm(string inputFileName, string outputFileName, ModeEncryption mode)
        {
            _currentProgress = 0;
            try
            {
                using (FileStream fsread = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {
                        Model2 part2 = new Model2();

                        var count = fsread.Length / 100;
                        if(count==0) throw new Exception("Пустой файл");
                        for (int i = 0; i < fsread.Length; i++)
                        {
                            if (_worker != null)
                                if (i % count == 0 && i != 0)
                                {
                                    _worker.ReportProgress(_currentProgress);
                                    Thread.Sleep(5);
                                    _currentProgress = _currentProgress + 1;
                                    _onPropertyChanged("CurrentProgress");

                                }

                            int readByte = fsread.ReadByte();
                            uint res;
                            if (mode == ModeEncryption.Encrypt)
                                res = part2.SwapBits((uint)readByte, new List<byte> {5, 3, 7, 1, 4, 0, 6, 2});
                            else
                                res = part2.SwapBits((uint)readByte, new List<byte> {5, 1, 7, 3, 6, 0, 4, 2});
                            fswrite.WriteByte(BitConverter.GetBytes(res)[0]);

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

        public string Vernam(string inputFileName, string outputFileName)
        {
            _currentProgress = 0;
            try
            {
                using (FileStream fsread = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {
                        Model2 part2 = new Model2();

                        var count = fsread.Length / 100;
                        if (count == 0) throw new Exception("Пустой файл");
                        for (int i = 0; i < fsread.Length; i++)
                        {
                            if (_worker != null)
                                if (i % count == 0 && i != 0)
                                {
                                    _worker.ReportProgress(_currentProgress);
                                    Thread.Sleep(1);
                                    _currentProgress = _currentProgress + 1;
                                    _onPropertyChanged("CurrentProgress");

                                }

                            int readByte = fsread.ReadByte();

                            uint res;
                            string key = "keykeykeykeykeykeykey";
                            Dictionary<char, int> alph = new Dictionary<char, int>();
                            Dictionary<int, char> alph_r = new Dictionary<int, char>();

                            for (int j = 0; j < 256; j++)
                            {
                                char c = (char) j;
                                alph.Add(c, j);
                                alph_r.Add(j, c);
                            }

                            int idx;
                            if (alph.TryGetValue((char)readByte, out idx))
                            {

                                res = (uint) alph_r[(idx ^ alph[key[i % key.Length]]) % alph.Count];
                                fswrite.WriteByte(BitConverter.GetBytes(res)[0]);
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

        #region MyDes
        

        public string MyImplementationDes(string inputFileName, string outputFileName, ModeEncryption mode)
        {
            _currentProgress = 0;
            MyDes md=new MyDes(ref _currentProgress,
             _worker, _onPropertyChanged);                
            
                if (mode == ModeEncryption.Encrypt)
                    return md.Encrypt(inputFileName, outputFileName);
                else
                    return md.Decipher(inputFileName, outputFileName);
          
        }
        #endregion


        #region Standart Des


        public string StandartDes(string inputFileName, string outputFileName, ModeEncryption mode)
        {
            _currentProgress = 0;
            StandartDes sd = new StandartDes(ref _currentProgress,
                _worker, _onPropertyChanged);

            if (mode == ModeEncryption.Encrypt)
                return sd.Encrypt(inputFileName, outputFileName);
            else
                return sd.Decipher(inputFileName, outputFileName);
        }
       
        
        #endregion





        #region Implementation RC4

      


        public string Rc4(string inputFileName, string outputFileName)
        {
            _currentProgress = 0;


            try
            {
                using (FileStream fsread = new FileStream(inputFileName, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fswrite = new FileStream(outputFileName, FileMode.Create, FileAccess.Write))
                    {
                        Model2 part2 = new Model2();

                        var count = fsread.Length / 100;
                        if (count == 0) throw new Exception("Пустой файл");


                        RC4 rc4=new RC4("keykeykeykeykeykeykey");
                    
                        for (int q = 0; q < fsread.Length; q++)
                        {
                            if (_worker != null)
                                if (q % count == 0 && q != 0)
                                {
                                    _worker.ReportProgress(_currentProgress);
                                    Thread.Sleep(1);
                                    _currentProgress = _currentProgress + 1;
                                    _onPropertyChanged("CurrentProgress");

                                }

                            int readByte = fsread.ReadByte();

                            uint result = rc4.EncodeByte((uint)readByte);

                            fswrite.WriteByte(BitConverter.GetBytes(result)[0]);
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

        #endregion







    }
}
