using System;
using System.ComponentModel;

namespace lab1rpks.Model.Interfaces
{
    
    public interface IEncryptionAlgorithm
    {
        string MyAlgorithm(string inputFileName, string outputFileName, ModeEncryption mode);//true if encoder  false if decoder
        string Vernam(string inputFileName, string outputFileName);//true if encoder  false if decoder
        string MyImplementationDes(string inputFileName, string outputFileName, ModeEncryption mode);//true if encoder  false if decoder
        string StandartDes(string inputFileName, string outputFileName, ModeEncryption mode);//true if encoder  false if decoder
        string Rc4(string inputFileName, string outputFileName);//true if encoder  false if decoder

    }
}
