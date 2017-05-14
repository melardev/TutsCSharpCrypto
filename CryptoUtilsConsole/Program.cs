using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtilsConsole
{
    class Program
    {

        private static RSAParameters publicKey;
        private static RSAParameters privateKey;
        static string CONTAINER_NAME = "MyContainerName";
        public enum KeySizes
        {
            SIZE_512 = 512,
            SIZE_1024 = 1024,
            SIZE_2048 = 2048,
            SIZE_952 = 952,
            SIZE_1369 = 1369
        };
        static void Main(string[] args)
        {
            string message = "The quick brown fox jumps over the lazy dog";
            generateKeys();
            byte[] encrypted = Encrypt(Encoding.UTF8.GetBytes(message));
            byte[] decrypted = Decrypt(encrypted);
            DeleteKeyInCSP();
            Console.WriteLine("Original\n\t " + message + "\n");
            Console.WriteLine("Encrypted\n\t" + BitConverter.ToString(encrypted).Replace("-", "") + "\n");
            Console.WriteLine("Decrypted\n\t" + Encoding.UTF8.GetString(decrypted));
            
            Console.ReadLine();

        }


        static void generateKeys()
        {
            int rsa_provider = 1;

            CspParameters cspParameters = new CspParameters(rsa_provider); //1 for rsa ; 13 for DSA ( Digital signature algorithm)
            cspParameters.KeyContainerName = CONTAINER_NAME;
            cspParameters.Flags = CspProviderFlags.UseMachineKeyStore;
            cspParameters.ProviderName = "Microsoft Strong Cryptographic Provider";
            var rsa = new RSACryptoServiceProvider(cspParameters);
            rsa.PersistKeyInCsp = true;
        }

        public static void DeleteKeyInCSP()
        {
            var cspParams = new CspParameters();
            cspParams.KeyContainerName = CONTAINER_NAME;
            var rsa = new RSACryptoServiceProvider(cspParams);
            rsa.PersistKeyInCsp = false;
            rsa.Clear();
        }


        private static byte[] Encrypt(byte[] plain)
        {
            byte[] encrypted;
            int rsa_provider = 1;
            CspParameters cspParameters = new CspParameters(rsa_provider);
            cspParameters.KeyContainerName = CONTAINER_NAME;

            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048, cspParameters))
            {
                encrypted = rsa.Encrypt(plain, true);
            }
            return encrypted;
        }

        private static byte[] Decrypt(byte[] encrypted)
        {
            byte[] decrypted;

            CspParameters cspParameters = new CspParameters();
            cspParameters.KeyContainerName = CONTAINER_NAME;

            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048, cspParameters))
            {
                decrypted = rsa.Decrypt(encrypted, true);
            }
            return decrypted;
        }

        /*
        
       private static void generateKeys(string publicKeyFile, string privateKeyFile)
        {
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
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
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
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
            using (var rsa = new RSACryptoServiceProvider((int)KeySizes.SIZE_2048))
            {
                rsa.PersistKeyInCsp = false;
                string privateKey = File.ReadAllText(privateKeyFile);
                rsa.FromXmlString(privateKey);
                decrypted = rsa.Decrypt(encrypted, true);
            }
            return decrypted;
        }*/

        /*
      
         static void generateKeys()
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

        

    */
    }


}

