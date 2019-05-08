using System;
using System.Collections.Generic;

using System.Windows;
using lab1rpks.Model.Interfaces;
using lab1rpks.Model.Interfaces.Implementations;

namespace lab1rpks.ViewModel
{
    public abstract class ApplicationViewModelPart2:ApplicationViewModelPart1
    {
        private readonly IModel2 _model2;

        protected ApplicationViewModelPart2()
        {
            _model2 = new Model2();
        }


        private uint _findDividerMaxDegree2;
        private string _findDividerMaxDegree2Str = "";    
        public String FindDividerMaxDegree2
        {
            get
            {
                if (_findDividerMaxDegree2Str != "")
                {
                    _answerFindDividerMaxDegree2 = _model2.FindDividerMaxDegree2((int)_findDividerMaxDegree2);
                    OnPropertyChanged($"AnswerFindDividerMaxDegree2");
                }
                return _findDividerMaxDegree2Str;
            }
            set
            {
                _findDividerMaxDegree2Str = value;
                var temp = _findDividerMaxDegree2;

                try
                {
                    _findDividerMaxDegree2 = Convert.ToUInt32(value);
                }
                catch (Exception)
                {
                    _findDividerMaxDegree2 = temp;                  
                }
            }
        }

        private int _answerFindDividerMaxDegree2;
        public String AnswerFindDividerMaxDegree2
        {
            get { return _answerFindDividerMaxDegree2.ToString(); }
        }



        private uint _findPFromX;
        private string _findPFromXStr = "";
        public String FindPFromX
        {
            get
            {
                if (_findPFromXStr != "")
                {
                    try
                    {
                        _answerFindPFromX = _model2.FindPFromX(_findPFromX);
                    }
                    catch (Exception e)
                    {
                        _answerFindPFromX = 0;
                        MessageBox.Show(e.Message);
                    }
                    
                    OnPropertyChanged($"AnswerFindPFromX");
                }
                return _findPFromXStr;
            }
            set
            {
                _findPFromXStr = value;
                var temp = _findPFromX;

                try
                {
                    _findPFromX = Convert.ToUInt32(value);
                }
                catch (Exception)
                {
                    _findPFromX = temp;
                }
            }
        }

        private uint _answerFindPFromX;
        public String AnswerFindPFromX
        {
            get { return _answerFindPFromX.ToString(); }
        }

        private uint _xorAllBits;
        private string _xorAllBitsXStr = "";
        public String XorAllBits
        {
            get
            {
                if (_xorAllBitsXStr != "")
                {
                    _answerXorAllBits = _model2.XorAllBits(_xorAllBits);
                    OnPropertyChanged($"AnswerXorAllBits");
                }
                return _xorAllBitsXStr;
            }
            set
            {
                _xorAllBitsXStr = value;
                var temp = _xorAllBits;

                try
                {
                    _xorAllBits = Convert.ToUInt32(value,2);
                }
                catch (Exception)
                {
                    _xorAllBits = temp;
                    if (value.Length == 32)
                        MessageBox.Show("Число может быть только 32 бита");
                    if (value.Length > 0)
                        MessageBox.Show("Вводите только 0 или 1");
                }
            }
        }

        private uint _answerXorAllBits;
        public String AnswerXorAllBits
        {
            get { return _answerXorAllBits.ToString(); }
        }



        private uint _cyclicShifNumber;
        private uint _cyclicShifCount;
        private string _cyclicShifStr = "";

        public String CyclicShif
        {
            get
            {
                if (_cyclicShifStr != "")
                {
                    var left= _model2.CyclicShiftLeft(_cyclicShifNumber, (int)_cyclicShifCount);
                    var right= _model2.CyclicShiftRight(_cyclicShifNumber, (int)_cyclicShifCount);
                    _answerCyclicShif = Convert.ToString(left, 2) + " , " + Convert.ToString(right, 2);
                    OnPropertyChanged($"AnswerCyclicShif");
                }

                return _cyclicShifStr;
            }
            set
            {
                _cyclicShifStr = value;
                var temp1 = _cyclicShifNumber;
                var temp2 = _cyclicShifCount;

                try
                {


                    var str = value.Split(',');
                    _cyclicShifNumber = Convert.ToUInt32(str[0]);
                    _cyclicShifCount = Convert.ToUInt32(str[1]);
                    if (_cyclicShifCount > 32) _cyclicShifCount = 32;
                }
                catch (Exception)
                {

                    _cyclicShifNumber = temp1;
                    _cyclicShifCount = temp2;
                }
            }

        }
        
        private String _answerCyclicShif;
        public String AnswerCyclicShif
        {
            get { return _answerCyclicShif; }
        }

        ///////////
        ///
        ///
        /// SwapBitsNumber
        ///SwapBitsRuleList
        ///
        private uint _swapBitsNumber;
      

        private string _swapBitsNumberStr = "";
        public String SwapBitsNumber
        {
            get
            {
                if (_swapBitsNumberStr != "")
                {
                    _answerSwapBits = _model2.SwapBits(_swapBitsNumber, _swapBitsRuleList);
                    OnPropertyChanged($"AnswerSwapBits");
                }

                return _swapBitsNumberStr;
            }
            set
            {
                _swapBitsNumberStr = value;
                var temp = _swapBitsNumber;

                try
                {

                    _swapBitsNumber = Convert.ToUInt32(value, 2);
                }
                catch (Exception)
                {
                    _swapBitsNumber = temp;
                    if (value.Length == 32)
                        MessageBox.Show("Число может быть только 32 бита");
                    if (value.Length > 0)
                        MessageBox.Show("Вводите только 0 или 1");
                }
            }

        }

        private List<byte> _swapBitsRuleList = new List<byte>();
        private string _swapBitsRuleListStr = "";
        public String SwapBitsRuleList
        {
            get
            {
                if (_swapBitsRuleListStr != "")
                {
                    _answerSwapBits = _model2.SwapBits(_swapBitsNumber, _swapBitsRuleList);
                    OnPropertyChanged($"AnswerSwapBits");
                }

                return _swapBitsRuleListStr;
            }
            set
            {
                _swapBitsRuleListStr = value;
                var temp = _swapBitsRuleList;

                try
                {
                    var val = value.Split(',');
                    _swapBitsRuleList.Clear();

                    foreach (var num in val)
                    {
                        byte t = Convert.ToByte(num);
                        if (t < 32)
                            _swapBitsRuleList.Add(t);
                    }




                }
                catch (Exception)
                {
                    _swapBitsRuleList = temp;

                }
            }

        }

        private uint _answerSwapBits;
        public String AnswerSwapBits
        {
            get { return Convert.ToString(_answerSwapBits, 2); }
        }

    }
}



