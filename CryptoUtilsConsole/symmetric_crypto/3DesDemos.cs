using System.IO;
using System.Security.Cryptography;

namespace CryptoUtilsConsole.symmetric_crypto
{
    class TripleDESDemos
    {

        static byte[] TripleDESCrypto(CryptoOperation cryptoOperation, byte[] IV, byte[] key, byte[] message)
        {
            using (var tripleDes = new TripleDESCryptoServiceProvider())
            {
                tripleDes.IV = IV;
                tripleDes.Key = key;
                tripleDes.Mode = CipherMode.CBC;
                tripleDes.Padding = PaddingMode.PKCS7;


                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = null;

                    if (cryptoOperation == CryptoOperation.ENCRYPT)
                        cryptoStream = new CryptoStream(memStream, tripleDes.CreateEncryptor(), CryptoStreamMode.Write);
                    else if (cryptoOperation == CryptoOperation.DECRYPT)
                        cryptoStream = new CryptoStream(memStream, tripleDes.CreateDecryptor(), CryptoStreamMode.Write);

                    if (cryptoStream == null)
                        return null;

                    cryptoStream.Write(message, 0, message.Length);
                    cryptoStream.FlushFinalBlock();
                    return memStream.ToArray();
                }
            }
        }

        public static void LaunchDemo()
        {
            
        }
    }
}
