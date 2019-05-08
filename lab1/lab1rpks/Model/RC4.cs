using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1rpks.Model.Interfaces.Implementations
{
    public class RC4
    {
        private string _key;
        private byte[] S = new byte[256];
        private int i = 0;
        private int j = 0;
        public RC4(string key)
        {

            _key = key;

            int i, j;
            for (i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + S[i] + _key[i % _key.Length]) % 256;
                swap(S, i, j);
            }
        }

        public uint EncodeByte(uint num)
        {
            return num ^ kword();
        }

        private void swap(byte[] array, int ind1, int ind2)
        {
            byte temp = array[ind1];
            array[ind1] = array[ind2];
            array[ind2] = temp;
        }

        //генератор псевдослучайной последовательности
        private byte kword()
        {
            i = (i + 1) % 256;
            j = (j + S[i]) % 256;
            swap(S, i, j);

            byte K = S[(S[i] + S[j]) % 256];
            return K;
        }
        
    }
}
