using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtilsConsole.AsymmetricCrypto
{
    public enum KeySizes
    {
        SIZE_512 = 512,
        SIZE_1024 = 1024,
        SIZE_2048 = 2048,
        SIZE_952 = 952,
        SIZE_1369 = 1369
    };

    class RsaFileDemo
    {
        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            string publicKey = "./pub.cert";
            string privateKey = "./priv.cert";
            GenerateKeys(publicKey, privateKey);
            byte[] encrypted = Encrypt(publicKey, Encoding.UTF8.GetBytes(message));
            byte[] decrypted = Decrypt(privateKey, encrypted);

            Console.WriteLine("Original\n\t " + message + "\n");
            Console.WriteLine("Encrypted\n\t" + BitConverter.ToString(encrypted).Replace("-", "") + "\n");
            Console.WriteLine("Decrypted\n\t" + Encoding.UTF8.GetString(decrypted));

            Console.ReadLine();
        }

        private static void GenerateKeys(string publicKeyFile, string privateKeyFile)
        {
            using (var rsa = new RSACryptoServiceProvider((int) KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;

                if (File.Exists(privateKeyFile))
                    File.Delete(privateKeyFile);

                if (File.Exists(publicKeyFile))
                    File.Delete(publicKeyFile);

                string publicKey = rsa.ToXmlString(false);
                File.WriteAllText(publicKeyFile, publicKey);
                string privateKey = rsa.ToXmlString(true);
                File.WriteAllText(privateKeyFile, privateKey);
            }
        }


        private static byte[] Encrypt(string publicKeyFile, byte[] plain)
        {
            byte[] encrypted;
            using (var rsa = new RSACryptoServiceProvider((int) KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;
                string publicKey = File.ReadAllText(publicKeyFile);
                rsa.FromXmlString(publicKey);
                encrypted = rsa.Encrypt(plain, true);
            }

            return encrypted;
        }

        private static byte[] Decrypt(string privateKeyFile, byte[] encrypted)
        {
            byte[] decrypted;
            using (var rsa = new RSACryptoServiceProvider((int) KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;
                string privateKey = File.ReadAllText(privateKeyFile);
                rsa.FromXmlString(privateKey);
                decrypted = rsa.Decrypt(encrypted, true);
            }

            return decrypted;
        }
    }
}