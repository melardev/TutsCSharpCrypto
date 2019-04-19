using System;
using System.Security.Cryptography;
using System.Text;


namespace CryptoUtilsConsole.AsymmetricCrypto
{
    class RsaInMemoryDemo
    {
        private static RSAParameters publicKey;
        private static RSAParameters privateKey;


        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            GenerateKeys();
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(message));
            byte[] decrypted = Decrypt(encrypted);
            
            Console.WriteLine("Original\n\t " + message + "\n");
            Console.WriteLine("Encrypted\n\t" + BitConverter.ToString(encrypted).Replace("-", "") + "\n");
            Console.WriteLine("Decrypted\n\t" + Encoding.UTF8.GetString(decrypted));

            Console.ReadLine();
        }

        static void GenerateKeys()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false; //Don't store the keys in a key container
                publicKey = rsa.ExportParameters(false);
                privateKey = rsa.ExportParameters(true);
            }
        }

        static byte[] Encrypt(byte[] input)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(publicKey);
                encrypted = rsa.Encrypt(input, true);
            }

            return encrypted;
        }

        static byte[] Decrypt(byte[] input)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.ImportParameters(privateKey);
                decrypted = rsa.Decrypt(input, true);
            }

            return decrypted;
        }
    }
}