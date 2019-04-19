using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtilsConsole.symmetric_crypto
{
    class AESDemos
    {
        public static byte[] AESCrypto(CryptoOperation cryptoOperation, AesCryptoServiceProvider aes, byte[] message)
        {
            using (var memStream = new MemoryStream())
            {
                CryptoStream cryptoStream = null;

                if (cryptoOperation == CryptoOperation.ENCRYPT)
                    cryptoStream = new CryptoStream(memStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                else if (cryptoOperation == CryptoOperation.DECRYPT)
                    cryptoStream = new CryptoStream(memStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                if (cryptoStream == null)
                    return null;

                cryptoStream.Write(message, 0, message.Length);
                cryptoStream.FlushFinalBlock();
                return memStream.ToArray();
            }
        }

        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                aes.GenerateKey();
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                byte[] encrypted = AESCrypto(CryptoOperation.ENCRYPT, aes, Encoding.UTF8.GetBytes(message));
                Console.WriteLine("Encrypted Text :" + BitConverter.ToString(encrypted).Replace("-", ""));
                byte[] decrypted = AESCrypto(CryptoOperation.DECRYPT, aes, encrypted);
                Console.WriteLine("Decrypted Text :" + Encoding.UTF8.GetString(decrypted));
            }

            Console.ReadLine();
        }
    }
}