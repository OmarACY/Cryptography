using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var textToCipher = "Texto para cifrado build";
            Console.WriteLine($"Texto a cifrar {textToCipher}");
            var textCipher = CryptographyService.Cipher(textToCipher);
            Console.WriteLine($"Texto cifrado {textCipher}");
            var textDecipher = CryptographyService.Decipher(textCipher);
            Console.WriteLine($"Texto descifrado {textDecipher}");

            Console.ReadKey();
        }
    }
}
