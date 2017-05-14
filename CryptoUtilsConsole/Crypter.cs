using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtilsConsole
{
    class Crypter
    {
        public enum Mode
        {
            ENCRYPT,
            DECRYPT
        };

        public static string getMD5(string message)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashmeHashed = md5.ComputeHash(Encoding.UTF8.GetBytes(message));
                string result = BitConverter.ToString(hashmeHashed).Replace("-", "");
                return (result);
            }
        }

        public static string getSHA1(string message)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] hashmeHashed = sha1.ComputeHash(Encoding.UTF8.GetBytes(message));
                string result = BitConverter.ToString(hashmeHashed).Replace("-", "");
                return (result);
            }
        }

        public static void getSHA(string message)
        {
            string hashme = "The quick brown fox jumps over the lazy dog";
            SHA1 sha1 = SHA1.Create();
            byte[] hashmeHashed = sha1.ComputeHash(Encoding.UTF8.GetBytes(hashme));
            string result = BitConverter.ToString(hashmeHashed).Replace("-", "");
            Console.WriteLine("SHA1 : " + result);


            SHA256 sha256 = SHA256.Create();
            hashmeHashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(hashme));
            result = BitConverter.ToString(hashmeHashed).Replace("-", "");
            Console.WriteLine("SHA256 : " + result);

            SHA512 sha512 = SHA512.Create();
            hashmeHashed = sha512.ComputeHash(Encoding.UTF8.GetBytes(hashme));
            result = BitConverter.ToString(hashmeHashed).Replace("-", "");
            Console.WriteLine("SHA512 : " + result);
        }

        public static void getHMACMD5(string message)
        {
            using (var randonNumberGenerator = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[32];
                randonNumberGenerator.GetBytes(key);
                using (var hmacmd5 = new HMACMD5(key))
                {
                    byte[] hashmeHashed = hmacmd5.ComputeHash(Encoding.UTF8.GetBytes(message));
                    string result = BitConverter.ToString(hashmeHashed).Replace("-", "");
                    Console.WriteLine("MD5 : " + result);
                }
            }
        }

        public static void getHMACSHA1(string message)
        {
            using (var randonNumberGenerator = new RNGCryptoServiceProvider())
            {
                byte[] key = new byte[32];
                randonNumberGenerator.GetBytes(key);
                Console.WriteLine("HMAC : " + BitConverter.ToString(key).Replace("-", ""));
                using (var hmacsha1 = new HMACSHA1(key))
                {
                    byte[] hashmeHashed = hmacsha1.ComputeHash(Encoding.UTF8.GetBytes(message));
                    string result = BitConverter.ToString(hashmeHashed).Replace("-", "");
                    Console.WriteLine("SHA1 : " + result);
                }


            }
        }

        static byte[] DESCrypto(Mode mode, byte[] IV, byte[] key, byte[] message)
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

                    if (mode == Mode.ENCRYPT)
                        cryptoStream = new CryptoStream(memStream, DES.CreateEncryptor(), CryptoStreamMode.Write);
                    else if (mode == Mode.DECRYPT)
                        cryptoStream = new CryptoStream(memStream, DES.CreateDecryptor(), CryptoStreamMode.Write);

                    if (cryptoStream == null)
                        return null;

                    cryptoStream.Write(message, 0, message.Length);
                    cryptoStream.FlushFinalBlock();
                    return memStream.ToArray();
                }
            }
        }



        static byte[] AESCrypto(Mode mode, AesCryptoServiceProvider aes, byte[] message)
        {
            using (var memStream = new MemoryStream())
            {
                CryptoStream cryptoStream = null;

                if (mode == Mode.ENCRYPT)
                    cryptoStream = new CryptoStream(memStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                else if (mode == Mode.DECRYPT)
                    cryptoStream = new CryptoStream(memStream, aes.CreateDecryptor(), CryptoStreamMode.Write);

                if (cryptoStream == null)
                    return null;

                cryptoStream.Write(message, 0, message.Length);
                cryptoStream.FlushFinalBlock();
                return memStream.ToArray();
            }
        }

        static byte[] TripleDESCrypto(Mode mode, byte[] IV, byte[] key, byte[] message)
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

                    if (mode == Mode.ENCRYPT)
                        cryptoStream = new CryptoStream(memStream, tripleDes.CreateEncryptor(), CryptoStreamMode.Write);
                    else if (mode == Mode.DECRYPT)
                        cryptoStream = new CryptoStream(memStream, tripleDes.CreateDecryptor(), CryptoStreamMode.Write);

                    if (cryptoStream == null)
                        return null;

                    cryptoStream.Write(message, 0, message.Length);
                    cryptoStream.FlushFinalBlock();
                    return memStream.ToArray();
                }
            }
        }

        static void test()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            var random = new Random();
            byte[] IV = new byte[8];
            random.NextBytes(IV);
            byte[] key = new byte[8];
            random.NextBytes(key);
            byte[] encrypted = DESCrypto(Crypter.Mode.ENCRYPT, IV, key, Encoding.UTF8.GetBytes(message));
            Console.WriteLine("Encrypted Text :" + BitConverter.ToString(encrypted).Replace("-", ""));
            byte[] decrypted = DESCrypto(Crypter.Mode.DECRYPT, IV, key, encrypted);
            Console.WriteLine("Decrypted Text :" + Encoding.UTF8.GetString(decrypted));

            bool baes = true;
            if (baes)
            {
                //string message = "The quick brown fox jumps over the lazy dog";

                using (var aes = new AesCryptoServiceProvider())
                {
                    aes.GenerateIV();
                    aes.GenerateKey();
                    aes.Mode = CipherMode.CBC;
                    aes.Padding = PaddingMode.PKCS7;

                    encrypted = AESCrypto(Mode.ENCRYPT, aes, Encoding.UTF8.GetBytes(message));
                    Console.WriteLine("Encrypted Text :" + BitConverter.ToString(encrypted).Replace("-", ""));
                    decrypted = AESCrypto(Mode.DECRYPT, aes, encrypted);
                    Console.WriteLine("Decrypted Text :" + Encoding.UTF8.GetString(decrypted));
                }

                Console.ReadLine();
            }
            if (baes)
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
    }
}
