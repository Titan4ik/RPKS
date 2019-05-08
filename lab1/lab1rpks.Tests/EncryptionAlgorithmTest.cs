using System;
using System.Diagnostics;
using System.IO;
using lab1rpks.Model;
using lab1rpks.Model.Interfaces.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lab1rpks.Tests
{
    [TestClass]
    public class EncryptionAlgorithmTest
    {
        private EncryptionAlgorithm _encryptionAlgorithm;

        [TestInitialize]
        public void SetupContext()
        {
            int k = 0;
            _encryptionAlgorithm = new EncryptionAlgorithm(ref k, null, null);

        }
        [TestMethod]
        public void MyAlgorithmTest()
        {
            bool result = true;
            string inFile = @"C:\Users\aleks\Desktop\test rpks\6.png";
            string out1File = @"C:\Users\aleks\Desktop\test rpks\6out1.png";
            string out2File = @"C:\Users\aleks\Desktop\test rpks\6out2.png";

            _encryptionAlgorithm.MyAlgorithm(inFile, out1File, ModeEncryption.Encrypt);
            _encryptionAlgorithm.MyAlgorithm(out1File, out2File, ModeEncryption.Decipher);
            try
            {
                using (FileStream fsread1 = new FileStream(inFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fsread2 = new FileStream(out2File, FileMode.Open, FileAccess.Read))
                    {
                      
                        if (fsread1.Length != fsread2.Length)
                        {
                            result = false;
                        }
                        else
                            for (int i = 0; i < fsread1.Length; i++)
                            {
                                if (fsread1.ReadByte() != fsread2.ReadByte())
                                {
                                    result = false;
                                    break;

                                }
                            }

                    }
                }
            }
            catch (Exception )
            {
                result = false;
            }
            File.Delete(out1File);
            File.Delete(out2File);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void VernamTest()
        {
            bool result = true;
            string inFile = @"C:\Users\aleks\Desktop\test rpks\6.png";
            string out1File = @"C:\Users\aleks\Desktop\test rpks\6out1.png";
            string out2File = @"C:\Users\aleks\Desktop\test rpks\6out2.png";

            _encryptionAlgorithm.Vernam(inFile, out1File);
            _encryptionAlgorithm.Vernam(out1File, out2File);
            try
            {
                using (FileStream fsread1 = new FileStream(inFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fsread2 = new FileStream(out2File, FileMode.Open, FileAccess.Read))
                    {

                        if (fsread1.Length != fsread2.Length)
                        {
                            result = false;
                        }
                        else
                            for (int i = 0; i < fsread1.Length; i++)
                            {
                                if (fsread1.ReadByte() != fsread2.ReadByte())
                                {
                                    result = false;
                                    break;

                                }
                            }

                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            File.Delete(out1File);
            File.Delete(out2File);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void MyImplementationDesTest()
        {
            bool result = true;
            string inFile = @"C:\Users\aleks\Desktop\test rpks\12.png";
            string out1File = @"C:\Users\aleks\Desktop\test rpks\12test1.png";
            string out2File = @"C:\Users\aleks\Desktop\test rpks\12test2.png";
            int k = 0;
            MyDes md=new MyDes(ref k,null,null);
            md.EncryptNoProgressDisplay(inFile, out1File);
            md.DecipherNoProgressDisplay(out1File, out2File);
            
                using (FileStream fsread1 = new FileStream(inFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fsread2 = new FileStream(out2File, FileMode.Open, FileAccess.Read))
                    {

                        if (fsread1.Length != fsread2.Length)
                        {
                        throw new Exception(fsread1.Length+" "+ fsread2.Length);
                            result = false;
                        }
                        else
                            for (int i = 0; i < fsread1.Length; i++)
                            {
                                if (fsread1.ReadByte() != fsread2.ReadByte())
                                {
                                    result = false;
                                    break;

                                }
                            }

                    }
                }
            

            File.Delete(out1File);
            File.Delete(out2File);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void StandartDesTest()
        {
            bool result = true;
            string inFile = @"C:\Users\aleks\Desktop\test rpks\test2.txt";
            string out1File = @"C:\Users\aleks\Desktop\test rpks\62.txt";
            string out2File = @"C:\Users\aleks\Desktop\test rpks\63.txt";

            int k = 0;
            StandartDes sd = new StandartDes(ref k, null, null);
            sd.EncryptNoProgressDisplay(inFile, out1File);
            sd.DecipherNoProgressDisplay(out1File, out2File);
            try
            {
                using (FileStream fsread1 = new FileStream(inFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fsread2 = new FileStream(out2File, FileMode.Open, FileAccess.Read))
                    {

                        if (fsread1.Length != fsread2.Length)
                        {
                           throw new Exception(fsread1.Length + " "+ fsread2.Length);
                            result = false;
                        }
                        else
                            for (int i = 0; i < fsread1.Length; i++)
                            {
                                if (fsread1.ReadByte() != fsread2.ReadByte())
                                {
                                    result = false;
          
                                    break;

                                }
                            }

                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            File.Delete(out1File);
            File.Delete(out2File);
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void Rc4Test()
        {
            bool result = true;
            string inFile = @"C:\Users\aleks\Desktop\test rpks\6.png";
            string out1File = @"C:\Users\aleks\Desktop\test rpks\6out1.png";
            string out2File = @"C:\Users\aleks\Desktop\test rpks\6out2.png";

            _encryptionAlgorithm.Rc4(inFile, out1File);
            _encryptionAlgorithm.Rc4(out1File, out2File);
            try
            {
                using (FileStream fsread1 = new FileStream(inFile, FileMode.Open, FileAccess.Read))
                {
                    using (FileStream fsread2 = new FileStream(out2File, FileMode.Open, FileAccess.Read))
                    {

                        if (fsread1.Length != fsread2.Length)
                        {
                            result = false;
                        }
                        else
                            for (int i = 0; i < fsread1.Length; i++)
                            {
                                if (fsread1.ReadByte() != fsread2.ReadByte())
                                {
                                    result = false;
                                    break;

                                }
                            }

                    }
                }
            }
            catch (Exception)
            {
                result = false;
            }
            File.Delete(out1File);
            File.Delete(out2File);
            Assert.IsTrue(result);
        }

      

    }
}
