using System;
using System.Collections.Generic;

namespace lab1rpks.Model.Interfaces.Implementations
{
    public class Model2 : IModel2
    {
        public int FindDividerMaxDegree2(int number)
        {
            return number & (-number);
        }

        public uint FindPFromX(uint p)
        {
            if (p == 0) throw new ArgumentException("Введите число больше 0");
            return  (uint)Math.Log(p, 2);

        }

        public uint XorAllBits(uint number)
        {
            uint result = 0;
            for (int i = 0; i < 32; i++)
            {
                result ^= (number << (31 - i)) >> 31;
            }

            return result;
        }

        public uint CyclicShiftLeft(uint number, int n)
        {         
            return (number >> (32 - n)) | (number << n);
        }

        public uint CyclicShiftRight(uint number, int n)
        {
            return (number<<(32-n))|(number>>n);
        }

        public uint SwapBits(uint number, List<byte> permutationRule) //стоит проверить что длина листа 32
        {
            uint result = 0;
           

            var temp = permutationRule.ToArray();
            for (int i = 0; i < permutationRule.Count; i++)
            {
                result |= ((number << (31 - temp[i])) >> 31) <<(permutationRule.Count - i-1);
            }
            

            return result;
        }

      
    }
}
