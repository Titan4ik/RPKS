using System.Collections.Generic;

namespace lab1rpks.Model.Interfaces
{
    public interface IModel2
    {
        int FindDividerMaxDegree2(int number);
        uint FindPFromX(uint p);
        uint XorAllBits(uint number);
        uint CyclicShiftLeft(uint number, int n);
        uint CyclicShiftRight(uint number, int n);
        uint SwapBits(uint number,  List<byte> permutationRule);
    }
}
