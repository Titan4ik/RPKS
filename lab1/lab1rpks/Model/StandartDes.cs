using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lab1rpks.Model.Interfaces.Implementations
{
    public class StandartDes
    {
        private int _currentProgress;
        private BackgroundWorker _worker;
        private Action<string> _onPropertyChanged;
        private const int sizeBlock = 8365568;

        public StandartDes(ref int currentProgress,
            BackgroundWorker worker, Action<string> onPropertyChanged)
        {
            _currentProgress = currentProgress;
            _worker = worker;
            _onPropertyChanged = onPropertyChanged;
        }

        public byte[] EncryptStandartDes(byte[] text, byte[] key, byte[] iv)
        {

            using (var desCryptoService = new DESCryptoServiceProvider())
            {
                desCryptoService.Key = key;
                desCryptoService.IV = iv;
                using (var memoryStream = new MemoryStream())
                {
                    var cryptoStream = new CryptoStream(memoryStream, desCryptoService.CreateEncryptor(),
                        CryptoStreamMode.Write);
                    cryptoStream.Write(text, 0, text.Length);
                    cryptoStream.Close();
                    memoryStream.Close();

                    return memoryStream.ToArray();
                }
            }
        }

        public byte[] DecryptStandartDes(byte[] encryptedText, byte[] key, byte[] iv)
        {

            using (var desCryptoService = new DESCryptoServiceProvider())
            {
                desCryptoService.Key = key;
                desCryptoService.IV = iv;
                var decryptor = desCryptoService.CreateDecryptor(desCryptoService.Key, desCryptoService.IV);
                using (var msDecrypt = new MemoryStream(encryptedText))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            byte[] res = new byte[msDecrypt.Length];

                            csDecrypt.Read(res, 0, res.Length);
                            return res;
                        }
                    }
                }
            }
        }

        private byte[] ByteArrayToRightLength(byte[] input)
        {
            byte[] res;
            if (input.Length == sizeBlock)
                res = input;
            else
            {
                res = new byte[sizeBlock];
                for (int i = 0; i < input.Length; i++)
                {
                    res[i] = input[i];
                }

                for (int i = input.Length; i < sizeBlock; i++)
                {
                    res[i] = 0;
                }
            }


            return res;
        }


        public string Encrypt(string inputFileName, string outputFileName)
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



                        oneTick = 100 / (double) (count);

                        for (int i = 0; i < count; i++)
                        {

                            byte[] readArr = new byte[sizeBlock];
                            readArr = fsread.ReadBytes(sizeBlock);


                            byte[] result;
                            byte[] key = new byte[] {1, 21, 1, 3, 41, 12, 25, 1};
                            byte[] iv = new byte[] {1, 22, 21, 43, 121, 212, 125, 10};

                            result = EncryptStandartDes(ByteArrayToRightLength(readArr), key, iv);
                            fswrite.Write(result, 0, result.Length);

                            progress += oneTick;
                            if (_currentProgress != (int) progress)
                            {
                                _currentProgress = (int) progress;
                                _worker.ReportProgress(_currentProgress);

                                Thread.Sleep(1);
                                _onPropertyChanged("CurrentProgress");
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
                        var count = length / (sizeBlock + 8);
                        if (count * (sizeBlock + 8) < length) count++;

                        oneTick = 100 / (double)(count);

                        for (int i = 0; i < count; i++)
                        {
                            byte[] readArr = new byte[sizeBlock + 8];
                            readArr = fsread.ReadBytes(sizeBlock + 8);
                            byte[] result;
                            byte[] key = new byte[] { 1, 21, 1, 3, 41, 12, 25, 1 };
                            byte[] iv = new byte[] { 1, 22, 21, 43, 121, 212, 125, 10 };
                            result = DecryptStandartDes(readArr, key, iv);
                            int countnullchar = 0;
                            if (i == count - 1)
                                for (int j = result.Length - 1; j >= 0; j--)
                                {
                                    if (result[j] == 0)
                                        countnullchar++;
                                    else
                                    {
                                        break;
                                    }
                                }
                            else
                            {
                                countnullchar = 8;
                            }
                            fswrite.Write(result, 0, result.Length - countnullchar);

                            progress += oneTick;
                            if (_currentProgress != (int)progress)
                            {
                                _currentProgress = (int)progress;
                                _worker.ReportProgress(_currentProgress);

                                Thread.Sleep(1);
                                _onPropertyChanged("CurrentProgress");
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


                            byte[] result;
                            byte[] key = new byte[] { 1, 21, 1, 3, 41, 12, 25, 1 };
                            byte[] iv = new byte[] { 1, 22, 21, 43, 121, 212, 125, 10 };

                            result = EncryptStandartDes(ByteArrayToRightLength(readArr), key, iv);
                            fswrite.Write(result, 0, result.Length);
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
                        var count = length / (sizeBlock+8);
                        if (count * (sizeBlock + 8) < length) count++;


                        for (int i = 0; i < count; i++)
                        {
                            byte[] readArr = new byte[sizeBlock+8];
                            readArr = fsread.ReadBytes(sizeBlock+8);
                            byte[] result;
                            byte[] key = new byte[] { 1, 21, 1, 3, 41, 12, 25, 1 };
                            byte[] iv = new byte[] { 1, 22, 21, 43, 121, 212, 125, 10 };
                            result = DecryptStandartDes(readArr, key, iv);
                            int countnullchar = 0;
                            if(i==count-1)
                                for (int j = result.Length - 1; j >= 0; j--)
                                {
                                    if (result[j] == 0)
                                        countnullchar++;
                                    else
                                    {
                                        break;
                                    }
                                }
                            else
                            {
                                countnullchar = 8;
                            }
                            fswrite.Write(result, 0, result.Length - countnullchar);
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
