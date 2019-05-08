using System;
using lab1rpks.Model.Interfaces.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace lab1rpks.Tests
{
    [TestClass]
    public class Model1Test
    {
        private Model1 _part1;

        [TestInitialize]
        public void SetupContext()
        {
            _part1 = new Model1();

        }


        [TestMethod]
        public void GetBitsInMiddleTest()
        {
            string btn = "11110111";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.GetBitsInMiddle(num, 8, 2);
            string btnResult = "1101";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void GlueTheBitsTest()
        {
            string btn = "11011111";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.GlueTheBits(num, 8, 2);
            string btnResult = "1111";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void RemoveKBitTest()
        {
            string btn = "11011111";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.RemoveKBit(num, 1);
            string btnResult = "11011101";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void ResetMLowBitTest()
        {
            string btn = "11011101";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.ResetMLowBit(num, 5);
            string btnResult = "11000000";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void SetKBitTest0()
        {
            string btn = "11011101";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.SetKBit(num, 0, 0);
            string btnResult = "11011100";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void SetKBitTest1()
        {
            string btn = "11011101";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.SetKBit(num, 1, 1);
            string btnResult = "11011111";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }
        [TestMethod]
        public void ShowKBitTest()
        {
            List<char> result = new List<char>();

            List<char> expected = new List<char>() { '0','1','0','1'};
            result.Add(_part1.ShowKBit(2, 0));
            result.Add(_part1.ShowKBit(2, 1));
            result.Add(_part1.ShowKBit(8, 1));
            result.Add(_part1.ShowKBit(11, 1));

           

         

            Assert.IsTrue(expected.SequenceEqual(result));
        }

        [TestMethod]
        public void SwapBitsTest()
        {
            string btn = "11011101";
            uint num = Convert.ToUInt32(btn, 2);
            uint result = _part1.SwapBits(num, 1, 4);
            string btnResult = "11001111";
            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }

        [TestMethod]
        public void SwapBytesTest()
        {
            string btn = "10110011100110101110011010101011";
            uint num = Convert.ToUInt32(btn, 2);

            uint result = _part1.SwapBytes(num, new List<byte> { 1,0,3,2 });


            string btnResult = "11100110101010111011001110011010";

            Assert.AreEqual(btnResult, Convert.ToString(result, 2));
        }
    }
}
