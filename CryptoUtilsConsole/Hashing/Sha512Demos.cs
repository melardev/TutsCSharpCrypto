using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtilsConsole.hashing
{
    class Sha512Demos
    {
        public static byte[] GetSha512Hash(string plainText)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] computedHash = sha512.ComputeHash(Encoding.UTF8.GetBytes(plainText));
            return computedHash;
        }


        public static string GetHash256Str(string message)
        {
            byte[] hashed = GetSha512Hash(message);
            string result = BitConverter.ToString(hashed).Replace("-", "");
            return result;
        }

        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            Console.WriteLine("SHA512 : " + GetHash256Str(message));
        }
    }
}