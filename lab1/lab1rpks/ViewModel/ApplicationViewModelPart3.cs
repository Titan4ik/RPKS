using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using lab1rpks.Model;
using lab1rpks.Model.Interfaces.Implementations;
using Microsoft.Win32;

namespace lab1rpks.ViewModel
{
    public class ApplicationViewModelPart3 : ApplicationViewModelPart2
    {
        private static bool _isRunning;

        EncryptionAlgorithm _encryptionAlgorithm;
        private BackgroundWorker worker = new BackgroundWorker();

        private SelectedAction _selectedAction;

        enum SelectedAction
        {
            IsNotChosen=0,
            IsMyEncryptionAlgorithm ,
            IsVernam,
            IsMyDes,
            IsStandartDes,
            IsRc4

        }

        public ApplicationViewModelPart3()
        {
            worker.DoWork += DoWork;
            worker.ProgressChanged += ProgressChanged;
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;
            CurrentProgress = -1;
            IsRunning = true;
            _selectedAction = SelectedAction.IsNotChosen;
            _encryptionAlgorithm = new EncryptionAlgorithm(ref _currentProgress, worker, OnPropertyChanged);

           
        }
        private void Test()
        {
            string inFile = @"C:\Users\aleks\Desktop\test rpks\12.png";
            string inFile2 = @"C:\Users\aleks\Desktop\test rpks\test2.txt";
            string inFile3 = @"C:\Users\aleks\Desktop\test rpks\test3.txt";
            string inFile4 = @"C:\Users\aleks\Desktop\test rpks\test4.txt";
            string inFile5 = @"C:\Users\aleks\Desktop\test rpks\test5.txt";
            string inFile6 = @"C:\Users\aleks\Desktop\test rpks\test6.txt";
            List<string> results = new List<string>();
          /*  results.Add(TestSpeed(inFile));
           
            results.Add("\n");
            results.Add(TestSpeed(inFile2));
            
            results.Add("\n");
            results.Add(TestSpeed(inFile3));
           
            results.Add("\n");
            results.Add(TestSpeed(inFile4));
            
            results.Add("\n");
            results.Add(TestSpeed(inFile5));*/
            
            results.Add("\n");
            results.Add(TestSpeed(inFile6));
            
            results.Add("\n");
            foreach (var str in results)
            {
                Console.WriteLine(str);
            }

        }
        private string TestSpeed(string inFile)
        {
            string result;

            string out1File = @"C:\Users\aleks\Desktop\test rpks\1.txt";
            string out2File = @"C:\Users\aleks\Desktop\test rpks\2.txt";
            int k = 0;
            long length = new System.IO.FileInfo(inFile).Length;
            result = "Size of file " + length + "\n";
            StandartDes sd = new StandartDes(ref k, null, null);
            MyDes md = new MyDes(ref k, null, null);

           

            

            System.Diagnostics.Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            md.EncryptNoProgressDisplay(inFile, out1File);
           
            sw2.Stop();
            result += "My desE=       " + (sw2.ElapsedMilliseconds / 100.0).ToString() + "\n";

            System.Diagnostics.Stopwatch sw3 = new Stopwatch();
            sw3.Start();
           
            md.DecipherNoProgressDisplay(out1File, out2File);
            sw3.Stop();
            result += "My desD=       " + (sw3.ElapsedMilliseconds / 100.0).ToString() + "\n";
            File.Delete(out1File);
            File.Delete(out2File);

            System.Diagnostics.Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            sd.EncryptNoProgressDisplay(inFile, out1File);
            sd.DecipherNoProgressDisplay(out1File, out2File);
            sw1.Stop();
            result += "Standart des= " + (sw1.ElapsedMilliseconds / 100.0).ToString() + "\n";


            File.Delete(out1File);
            File.Delete(out2File);
            return result;
        }
        private ICommand _radioCommand;

        public ICommand RadioCommand
        {
            get
            {
                if (_radioCommand == null)
                    _radioCommand = new RelayCommand((param) => { RadioMethod(param); });

                return _radioCommand;
            }
        }


        private void RadioMethod(object parametr)
        {
            //Test();

            switch (parametr.ToString())
            {
                case "MyEncryptionAlgorithm":
                    _selectedAction = SelectedAction.IsMyEncryptionAlgorithm;
                    break;
                case "Vernam":
                    _selectedAction = SelectedAction.IsVernam;
                    break;
                case "MyDes":
                    _selectedAction = SelectedAction.IsMyDes;
                    break;
                case "StandartDes":
                    _selectedAction = SelectedAction.IsStandartDes;
                    break;
                case "RC4":
                    _selectedAction = SelectedAction.IsRc4;
                    break;

            }


        }

        private String _inputFileName = "";
        private String _outputFileName = "";
     
        public String InputFileName
        {
            get { return _inputFileName; }
            set { _inputFileName = value; }
        }

        public String OutputFileName
        {
            get { return _outputFileName; }
            set { _outputFileName = value; }
        }


        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            CurrentProgress = e.ProgressPercentage;
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            if (_selectedAction == SelectedAction.IsNotChosen)
            {
                IsRunning = true;
                OnPropertyChanged($"IsRunning");
                MessageBox.Show("Не выбран метод шифрования");
                Thread.Sleep(1);
                return;
            }
               
            if (_inputFileName.Length == 0 || _outputFileName.Length == 0)
            {
                IsRunning = true;
                OnPropertyChanged($"IsRunning");
                MessageBox.Show("Пути до файлов не заданы");
                Thread.Sleep(1);
                return;
            }

            
            CurrentProgress = 0;
            OnPropertyChanged($"CurrentProgress");
            string result = "";
        
            switch (_selectedAction)
            {
                case SelectedAction.IsMyEncryptionAlgorithm:
                    result = _encryptionAlgorithm.MyAlgorithm(_inputFileName, _outputFileName, _modeEncryption);
                    break;
                case SelectedAction.IsMyDes:
                    result = _encryptionAlgorithm.MyImplementationDes(_inputFileName, _outputFileName, _modeEncryption);
                    break;
                case SelectedAction.IsStandartDes:
                    result = _encryptionAlgorithm.StandartDes(_inputFileName, _outputFileName, _modeEncryption);
                    break;
                case SelectedAction.IsVernam:
                    result = _encryptionAlgorithm.Vernam(_inputFileName, _outputFileName);
                    break;
                case SelectedAction.IsRc4:
                    result = _encryptionAlgorithm.Rc4(_inputFileName, _outputFileName);
                    break;
              
            }




            if (result == "")
            {
                CurrentProgress = 100;
                Thread.Sleep(1);
                OnPropertyChanged($"CurrentProgress");
            }         
            else
            {
                MessageBox.Show(result);
                CurrentProgress = -1;
                Thread.Sleep(1);
            }

            IsRunning = true;
            OnPropertyChanged($"IsRunning");
        }


        private ICommand _inputFileCommand;

        public ICommand InputFileCommand
        {
            get
            {
                return _inputFileCommand ?? (_inputFileCommand = new RelayCommand(x =>
                {

                    var dialog = new OpenFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        _inputFileName = dialog.FileName;
                        if (_outputFileName.Length != 0 &&
                            Path.GetExtension(_inputFileName) != Path.GetExtension(_outputFileName))
                        {
                            _outputFileName=_outputFileName.Replace(Path.GetExtension(_outputFileName), "");
                            _outputFileName += Path.GetExtension(_inputFileName);
                            OnPropertyChanged($"OutputFileName");
                        }
                            
                        OnPropertyChanged($"InputFileName");

                    }
                }));
            }
        }

        private ICommand _outputFileCommand;

        public ICommand OutputFileCommand
        {
            get
            {
                return _outputFileCommand ?? (_outputFileCommand = new RelayCommand(x =>
                {

                    var dialog = new SaveFileDialog();
                    if (dialog.ShowDialog() == true)
                    {
                        _outputFileName = dialog.FileName;
                        if (_inputFileName.Length != 0 && Path.GetExtension(_inputFileName)!= Path.GetExtension(_outputFileName))
                            _outputFileName+=Path.GetExtension(_inputFileName);
                        OnPropertyChanged($"OutputFileName");

                    }
                }));
            }
        }
        private ICommand _cryptCommand;
        private ModeEncryption _modeEncryption;

        public ICommand CryptCommand
        {
            get
            {
                return _cryptCommand ?? (_cryptCommand = new RelayCommand(x =>
                {


                    if (_isRunning)
                    {
                        _modeEncryption = ModeEncryption.Encrypt;
                        worker.RunWorkerAsync();
                        IsRunning = !IsRunning;
                        OnPropertyChanged($"IsRunning");
                    }
                    

                }));
            }
        }
        private ICommand _decryptCommand;
        public ICommand DecryptCommand
        {
            get
            {
                return _decryptCommand ?? (_decryptCommand = new RelayCommand(x =>
                {


                    if (_isRunning)
                    {
                        _modeEncryption = ModeEncryption.Decipher;
                        worker.RunWorkerAsync();
                        IsRunning = !IsRunning;
                        
                        OnPropertyChanged($"IsRunning");
                    }

                }));
            }
        }

        private int _currentProgress=-1;
        public int CurrentProgress
        {
            get
            {
                OnPropertyChanged($"StatusStr");
                return _currentProgress==-1?0: _currentProgress;

            }
            private set
            {
                
                    _currentProgress = value;
                    OnPropertyChanged($"StatusStr");

            }
        }

        public String StatusStr
        {
            get
            {
                if(_currentProgress==100)
                    return "Процесс завершен";
                else if (_currentProgress !=-1)
                    return "Процесс запущен ( " + _currentProgress.ToString() + " %), Ожидайте...";
                else
                {
                    return "";
                }
                
            }

        }

        public bool IsRunning
        {
            get { return _isRunning; }
            private set
            {
                
                    _isRunning = value;
                
            }
        }




    }
}
