using System.Collections.Generic;

namespace lab1rpks.Model.Interfaces
{
    public interface IModel1
    {
        char ShowKBit(uint number, int bitNumber);//должжен вернуть bit
        uint SetKBit(uint number,int k, uint bitValue);//должжен вернуть изщмененный number
        uint RemoveKBit(uint number, int k);//должжен вернуть изщмененный number
        uint SwapBits(uint number, int i, int j);
        uint ResetMLowBit(uint number, int m);

        uint GlueTheBits(uint number, int len, int i);
        uint GetBitsInMiddle(uint number, int len, int i);
        uint SwapBytes(uint number,List<byte> permutationRule);//стоит проверить что длина листа 32
    }
}
