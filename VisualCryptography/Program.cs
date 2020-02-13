using PGM_Reader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCryptography
{
    class Program
    {
        static void Main(string[] args)
        {
            PGM_File file = PGM_File.ReadFile("test.pgm");
            PGM_File[] shares = VisualSecretSplit.DivideIntoTwoShares(file);

            shares[0].saveToFile("share0.pgm");
            shares[1].saveToFile("share1.pgm");


            PGM_File secret = VisualSecretRecovery.RecoverSecretFromTwoShares(shares[0], shares[1]);
            secret.saveToFile("secret.pgm"); 
        }
    }
}
