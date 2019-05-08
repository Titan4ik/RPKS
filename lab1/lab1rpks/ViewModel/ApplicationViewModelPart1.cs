using System;
using System.Collections.Generic;
using System.Windows;
using lab1rpks.Model.Interfaces;
using lab1rpks.Model.Interfaces.Implementations;

namespace lab1rpks.ViewModel
{
    public abstract class ApplicationViewModelPart1 : BaseAplicationViewModel
    {
        private readonly IModel1 _part1;

        protected ApplicationViewModelPart1()
        {
            _part1 = new Model1();
        }

        private uint _number;
        private string _numberStr = "";

        public String Number
        {
            get
            {
                if (_numberStr != "")
                {
                    _numberStr = Convert.ToString(_number, 2);
                    OnPropertyChanged($"KBit");
                    OnPropertyChanged($"SetKBit");
                    OnPropertyChanged($"RemoveKBit");
                    OnPropertyChanged($"SwapBitsij");
                    OnPropertyChanged($"ResetMLowBit");
                    OnPropertyChanged($"GlueTheBits");
                    OnPropertyChanged($"GetBitsInMiddle");
                    OnPropertyChanged($"SwapBytes");

                }

                return _numberStr;
            }
            set
            {

                _numberStr = value;
                var temp = _number;

                try
                {


                    _number = Convert.ToUInt32(value, 2);

                }
                catch (Exception)
                {
                    _number = temp;
                    if (value.Length == 32)
                        MessageBox.Show("Число может быть только 32 бита");
                    if (value.Length > 0)
                        MessageBox.Show("Вводите только 0 или 1");
                }

            }

        }

        private uint _kBit;
        private string _kBitStr = "";

        public String KBit
        {
            get
            {

                if (_kBitStr != "")
                {
                    _answerKBit = _part1.ShowKBit(_number, (int) _kBit);
                    OnPropertyChanged($"AnswerKBit");
                }

                return _kBitStr;
            }
            set
            {

                _kBitStr = value;
                var temp = _kBit;

                try
                {


                    _kBit = Convert.ToUInt32(value);
                    if (_kBit > 31) _kBit = 31;

                }
                catch (Exception)
                {
                    _kBit = temp;
                    if (value.Length > 0)
                        MessageBox.Show("Вводите только цифры");
                }


            }

        }

        private char _answerKBit = '0';

        public String AnswerKBit
        {
            get { return _answerKBit.ToString(); }
        }


        private uint _setKBit;
        private uint _setKBitNumber;
        private string _setKBitStr = "";

        public String SetKBit
        {
            get
            {
                if (_setKBitStr != "")
                {
                    _answerSetKBit = _part1.SetKBit(_number, (int) _setKBit, _setKBitNumber);
                    OnPropertyChanged($"AnswerSetKBit");
                }

                return _setKBitStr;
            }
            set
            {
                _setKBitStr = value;
                var temp1 = _setKBit;
                var temp2 = _setKBitNumber;
                try
                {

                    var str = value.Split(',');
                    _setKBit = Convert.ToUInt32(str[0]);
                    _setKBitNumber = Convert.ToUInt32(str[1]);
                    if (_setKBit > 31) _setKBit = 31;
                }
                catch (Exception)
                {
                    _setKBitNumber = temp2;
                    _setKBit = temp1;
                }



            }

        }

        private uint _answerSetKBit;

        public String AnswerSetKBit
        {
            get { return Convert.ToString(_answerSetKBit, 2); }
        }


        private uint _removeKBit;
        private string _removeKBitStr = "";

        public String RemoveKBit
        {
            get
            {
                if (_removeKBitStr != "")
                {
                    _answerRemoveKBit = _part1.RemoveKBit(_number, (int) _removeKBit);
                    OnPropertyChanged($"AnswerRemoveKBit");
                }

                return _removeKBitStr;
            }
            set
            {
                _removeKBitStr = value;
                var temp = _removeKBit;

                try
                {


                    _removeKBit = Convert.ToUInt32(value);

                    if (_removeKBit > 31) _removeKBit = 31;
                }
                catch (Exception)
                {

                    _removeKBit = temp;
                }



            }

        }

        private uint _answerRemoveKBit;

        public String AnswerRemoveKBit
        {
            get { return Convert.ToString(_answerRemoveKBit, 2); }
        }


        private uint _swapBitsI;
        private uint _swapBitsJ;
        private string _swapBitsStr = "";

        public String SwapBitsij
        {
            get
            {
                if (_swapBitsStr != "")
                {
                    _answerSwapBits = _part1.SwapBits(_number, (int) _swapBitsI, (int) _swapBitsJ);
                    OnPropertyChanged($"AnswerSwapBitsij");
                }

                return _swapBitsStr;
            }
            set
            {
                _swapBitsStr = value;
                var temp1 = _swapBitsI;
                var temp2 = _swapBitsJ;

                try
                {


                    var str = value.Split(',');
                    _swapBitsI = Convert.ToUInt32(str[0]);
                    _swapBitsJ = Convert.ToUInt32(str[1]);
                    if (_swapBitsI > 31) _swapBitsI = 31;
                    if (_swapBitsJ > 31) _swapBitsJ = 31;
                }
                catch (Exception)
                {

                    _swapBitsI = temp1;
                    _swapBitsJ = temp2;
                }


            }

        }

        private uint _answerSwapBits;

        public String AnswerSwapBitsij
        {
            get { return Convert.ToString(_answerSwapBits, 2); }
        }


        private uint _resetMLowBit;
        private string _resetMLowBitStr = "";

        public String ResetMLowBit
        {
            get
            {
                if (_resetMLowBitStr != "")
                {
                    _answerResetMLowBit = _part1.ResetMLowBit(_number, (int) _resetMLowBit);
                    OnPropertyChanged($"AnswerResetMLowBit");
                }

                return _resetMLowBitStr;
            }
            set
            {
                _resetMLowBitStr = value;
                var temp = _resetMLowBit;

                try
                {


                    _resetMLowBit = Convert.ToUInt32(value);

                    if (_resetMLowBit > 31) _resetMLowBit = 31;
                }
                catch (Exception)
                {

                    _resetMLowBit = temp;
                }



            }

        }

        private uint _answerResetMLowBit;

        public String AnswerResetMLowBit
        {
            get { return Convert.ToString(_answerResetMLowBit, 2); }
        }


        private uint _glueTheBitsI;
        private uint _glueTheBitsLen;
        private string _glueTheBitsStr = "";

        public String GlueTheBits
        {
            get
            {
                if (_glueTheBitsStr != "")
                {
                    _answerGlueTheBits = _part1.GlueTheBits(_number, (int) _glueTheBitsLen, (int) _glueTheBitsI);
                    OnPropertyChanged($"AnswerGlueTheBits");
                }

                return _glueTheBitsStr;
            }
            set
            {
                _glueTheBitsStr = value;
                var temp1 = _glueTheBitsI;
                var temp2 = _glueTheBitsLen;

                try
                {


                    var str = value.Split(',');
                    _glueTheBitsI = Convert.ToUInt32(str[0]);
                    _glueTheBitsLen = Convert.ToUInt32(str[1]);
                    if (_glueTheBitsI > 31) _glueTheBitsI = 31;
                    if (_glueTheBitsLen > 31) _glueTheBitsLen = 31;
                }
                catch (Exception)
                {

                    _glueTheBitsI = temp1;
                    _glueTheBitsLen = temp2;
                }
            }

        }

        private uint _answerGlueTheBits;

        public String AnswerGlueTheBits
        {
            get { return Convert.ToString(_answerGlueTheBits, 2); }
        }


        private uint _getBitsInMiddleI;
        private uint _getBitsInMiddleLen;
        private string _getBitsInMiddleStr = "";

        public String GetBitsInMiddle
        {
            get
            {
                if (_getBitsInMiddleStr != "")
                {
                    _answerGetBitsInMiddle =
                        _part1.GetBitsInMiddle(_number, (int) _getBitsInMiddleLen, (int) _getBitsInMiddleI);
                    OnPropertyChanged($"AnswerGetBitsInMiddle");
                }

                return _getBitsInMiddleStr;
            }
            set
            {
                _getBitsInMiddleStr = value;
                var temp1 = _getBitsInMiddleI;
                var temp2 = _getBitsInMiddleLen;

                try
                {


                    var str = value.Split(',');
                    _getBitsInMiddleI = Convert.ToUInt32(str[0]);
                    _getBitsInMiddleLen = Convert.ToUInt32(str[1]);
                    if (_getBitsInMiddleI > 31) _getBitsInMiddleI = 31;
                    if (_getBitsInMiddleLen > 31) _getBitsInMiddleLen = 31;
                }
                catch (Exception)
                {

                    _getBitsInMiddleI = temp1;
                    _getBitsInMiddleLen = temp2;
                }
            }

        }

        private uint _answerGetBitsInMiddle;

        public String AnswerGetBitsInMiddle
        {
            get { return Convert.ToString(_answerGetBitsInMiddle, 2); }
        }


        private List<byte> _swapBytes = new List<byte>();
        private string _swapBytesStr = "";

        public String SwapBytes
        {
            get
            {
                if (_swapBytesStr != "" && _swapBytes.Count == 4)
                {
                    _answerSwapBytes = _part1.SwapBytes(_number, _swapBytes);
                    OnPropertyChanged($"AnswerSwapBytes");
                }

                return _swapBytesStr;
            }
            set
            {
                _swapBytesStr = value;
                var temp = _swapBytes;

                try
                {
                    var val = value.Split(',');
                    if (val.Length == 4)
                    {
                        _swapBytes.Clear();
                        foreach (var num in val)
                        {
                            byte t = Convert.ToByte(num);
                            if (t < 4)
                                _swapBytes.Add(t);
                        }
                    }

                    if (_swapBytes.Count != 4)
                        _swapBytes.Clear();
                }
                catch (Exception)
                {

                    _swapBytes = temp;
                }



            }

        }

        private uint _answerSwapBytes;

        public String AnswerSwapBytes
        {
            get { return Convert.ToString(_answerSwapBytes, 2); }
        }
    }
}
