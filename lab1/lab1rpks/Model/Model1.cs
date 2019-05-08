using System;
using System.Collections.Generic;

namespace lab1rpks.Model.Interfaces.Implementations
{
    public class Model1 : IModel1
    {

        public char ShowKBit(uint number, int bitNumber)
        {
            char c = '0';
            if ((number << (31 - bitNumber)) >> 31 == 1)
                c = '1';
            return c;
        }

        public uint SetKBit(uint number, int k, uint bitValue)
        {
            uint result = number;
            if (bitValue == 1)
            {
                result |= ((bitValue << 31) >> 31) << k;
            }
            else
            {
                result &= ~((uint)1 << k);
            }
            return result;
        }
        public uint RemoveKBit(uint number, int k)
        {
            uint result = number;

            result &= ~((uint)1 << k);

            return result;
        }

        public uint SwapBits(uint number, int i, int j)
        {
            uint result;
            uint valueI = number << (31 - i) >> 31;
            uint valueJ = number << (31 - j) >> 31;
            result = SetKBit(number, i, valueJ);
            result = SetKBit(result, j, valueI);
            

            return result;
        }

        public uint ResetMLowBit(uint number, int m)
        {
            uint result = number;
            uint value1 = ((uint) 1 << m );
            value1 -= 1;
            result &= ~value1;

            return result;
        }

        public uint GlueTheBits(uint number, int len, int i)
        {
            uint result = 0;
            uint temp= (number << (32 - len))>>(32-len);
            result |= temp << (32 - i) >> (32 - i); 
            result |= temp >>(len-i)<<i; 

            return result;
        }

        public uint GetBitsInMiddle(uint number, int len, int i)
        {
            uint result = (number << (32 - len)) >> (32 - len);
            result = result >> i;
            result = result << (32 - (len-2*i))>> (32 - (len - 2 * i));
           

            return result;
        }

        public uint SwapBytes(uint number, List<byte> permutationRule)
        {
            uint result = 0;
            if (permutationRule.Count != 4)
            {
                throw new ArgumentException("permutationRule.Count != 4");
            }

            var temp = permutationRule.ToArray();
            for (int i = 0; i < 4; i++)
            {
                uint val = number << (32 - 8 * (temp[i] + 1)) >> 24;
                result |= val<< (32 - 8 * (i + 1));
            }


            return result;
        }
    }
}
