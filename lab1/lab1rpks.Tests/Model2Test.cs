using System;
using System.Collections.Generic;
using lab1rpks.Model.Interfaces.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace lab1rpks.Tests
{
    [TestClass]
    public class Model2Test
    {
        private Model2 _part2;

        [TestInitialize]
        public void SetupContext()
        {
            _part2 = new Model2();
          
        }

        [TestMethod]
        public void FindDividerMaxDegree2Test()
        {
            List<int> result=new List<int>();

            List<int> expected=new List<int>(){1,64,2};

            result.Add(_part2.FindDividerMaxDegree2(65));
            result.Add(_part2.FindDividerMaxDegree2(64));
            result.Add(_part2.FindDividerMaxDegree2(6));

            Assert.IsTrue(expected.SequenceEqual(result));
        }
      
       
        [TestMethod]
        public void FindXFromPTest()
        {
            List<uint> result = new List<uint>();

            List<uint> expected = new List<uint>() { 2,6,3 };

            result.Add(_part2.FindPFromX(5));
            result.Add(_part2.FindPFromX(65));
            result.Add(_part2.FindPFromX(9));

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void XorAllBitsTest()
        {
            string btn1 = "101110001";
            string btn2 = "11100111";

            List<uint> result = new List<uint>();

            List<uint> expected = new List<uint>() { 1,0 };


            result.Add(_part2.XorAllBits(Convert.ToUInt32(btn1, 2)));
            result.Add(_part2.XorAllBits(Convert.ToUInt32(btn2, 2)));


            Assert.IsTrue(expected.SequenceEqual(result));


        }

        [TestMethod]
        public void CyclicShiftLeftTest()
        {
            string btn = "11000000000000000000000000001101";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part2.CyclicShiftLeft(num, 3);
            string btnResult = "1101110";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void CyclicShiftRightTest()
        {
            string btn = "11000000000000000000000000001101";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part2.CyclicShiftRight(num, 3);
            string btnResult = "10111000000000000000000000000001";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void SwapBitsTest()
        {
            string btn = "10101110";
            uint num = Convert.ToUInt32(btn, 2);

            uint result = _part2.SwapBits(num,new List<byte>{5,3,7,1,4,0,6,2});


            string btnResult = "11110001";

            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }
    }
}
