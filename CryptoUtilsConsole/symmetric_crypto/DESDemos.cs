using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtilsConsole.symmetric_crypto
{
    class DESDemos
    {
        public static byte[] DESCrypto(CryptoOperation cryptoOperation, byte[] IV, byte[] key, byte[] message)
        {
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.IV = IV;
                DES.Key = key;
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;


                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = null;

                    if (cryptoOperation == CryptoOperation.ENCRYPT)
                        cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
                    else if (cryptoOperation == CryptoOperation.DECRYPT)
                        cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(), CryptoStreamMode.Write);

                    if (cryptoStream == null)
                        return null;

                    cryptoStream.Write(message, 0, message.Length);
                    cryptoStream.FlushFinalBlock();
                    return memStream.ToArray();
                }
            }
        }


        static void EncryptFile(string filePath, string key)
        {
            byte[] plainContent = File.ReadAllBytes(filePath);
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.IV = Encoding.UTF8.GetBytes(key);
                DES.Key = Encoding.UTF8.GetBytes(key);
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;


                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(plainContent, 0, plainContent.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(filePath, memStream.ToArray());
                    Console.WriteLine("Encrypted succesfully " + filePath);
                }
            }
        }

        private static void DecryptFile(string filePath, string key)
        {
            byte[] encrypted = File.ReadAllBytes(filePath);
            using (var DES = new DESCryptoServiceProvider())
            {
                DES.IV = Encoding.UTF8.GetBytes(key);
                DES.Key = Encoding.UTF8.GetBytes(key);
                DES.Mode = CipherMode.CBC;
                DES.Padding = PaddingMode.PKCS7;


                using (var memStream = new MemoryStream())
                {
                    CryptoStream cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(),
                        CryptoStreamMode.Write);

                    cryptoStream.Write(encrypted, 0, encrypted.Length);
                    cryptoStream.FlushFinalBlock();
                    File.WriteAllBytes(filePath, memStream.ToArray());
                    Console.WriteLine("Decrypted succesfully " + filePath);
                }
            }
        }


        public static byte[] GenerateRandomByteArray(int size)
        {
            var random = new Random();
            byte[] byteArray = new byte[size];
            random.NextBytes(byteArray);
            return byteArray;
        }

        public static byte[] GenerateIv()
        {
            byte[] IV = GenerateRandomByteArray(8);
            return IV;
        }

        public static byte[] GenerateKey()
        {
            byte[] key = GenerateRandomByteArray(8);
            return key;
        }

        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";

            byte[] IV = GenerateIv();
            byte[] key = GenerateKey();

            byte[] encrypted = DESCrypto(CryptoOperation.ENCRYPT, IV, key, Encoding.UTF8.GetBytes(message));
            Console.WriteLine("Encrypted Text :" + BitConverter.ToString(encrypted).Replace("-", ""));
            byte[] decrypted = DESCrypto(CryptoOperation.DECRYPT, IV, key, encrypted);
            Console.WriteLine("Decrypted Text :" + Encoding.UTF8.GetString(decrypted));
        }

        public static void LaunchFileDemo()
        {
            string filePath = "D:\\Users\\rabheus\\Desktop\\CPSC\\tutos\\sampletxt.txt";
            string input;
            string keyy = "youtubee";
            while (true)
            {
                Console.WriteLine("a) Encrypt");
                Console.WriteLine("b) Decrypt");
                Console.WriteLine("c) exit");

                input = Console.ReadLine();
                if (input == "c")
                    break;
                else
                {
                    if (input == "a")
                        EncryptFile(filePath, keyy);
                    else if (input == "b")
                        DecryptFile(filePath, keyy);
                }
            }

        }
    }
}