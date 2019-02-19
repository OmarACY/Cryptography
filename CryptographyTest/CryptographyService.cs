using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptographyTest
{
    public class CryptographyService
    {
        private const string Key = "td3scrypt053rvic3";
        private static readonly char[] Padding = { '=' };

        public static string Cipher(string textToCipher)
        {
            try
            {
                var arrayToCipher = Encoding.UTF8.GetBytes(textToCipher);

                // Are used the classes for cipher MD5
                var hashMd5 = new MD5CryptoServiceProvider();
                var keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(Key));

                hashMd5.Clear();

                //Algorithm TripleDES
                var tripleDes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tripleDes.CreateEncryptor();
                var resultArray = cTransform.TransformFinalBlock(arrayToCipher, 0, arrayToCipher.Length);

                tripleDes.Clear();

                // return result in string form
                textToCipher = Base64SafeEncoding(Convert.ToBase64String(resultArray, 0, resultArray.Length));
            }
            catch (Exception)
            {
                // ignored
            }

            return textToCipher;
        }

        public static string Decipher(string textToDecipher)
        {
            try
            {
                textToDecipher = Base64SafeDecoding(textToDecipher);
                var arrayToDecipher = Convert.FromBase64String(textToDecipher);

                // Are used the classes for cipher MD5
                var hashMd5 = new MD5CryptoServiceProvider();
                var keyArray = hashMd5.ComputeHash(Encoding.UTF8.GetBytes(Key));

                hashMd5.Clear();

                //Algorithm TripleDES
                var tripleDes = new TripleDESCryptoServiceProvider
                {
                    Key = keyArray,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                };

                var cTransform = tripleDes.CreateDecryptor();
                var resultArray = cTransform.TransformFinalBlock(arrayToDecipher, 0, arrayToDecipher.Length);

                tripleDes.Clear();

                // return result in string form
                textToDecipher = Encoding.UTF8.GetString(resultArray);

            }
            catch (Exception)
            {
                textToDecipher = "";
            }

            return textToDecipher;
        }

        private static string Base64SafeEncoding(string base64)
        {
            return base64.TrimEnd(Padding).Replace('+', '-').Replace('/', '_');
        }

        private static string Base64SafeDecoding(string base64Safe)
        {
            var base64 = base64Safe.Replace('_', '/').Replace('-', '+');

            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return base64;
        }
    }
}
