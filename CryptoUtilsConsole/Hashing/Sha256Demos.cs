using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CryptoUtilsConsole.hashing
{
    class Sha256Demos
    {
        public static byte[] GetHash256(string message)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] computedHas = sha256.ComputeHash(Encoding.UTF8.GetBytes(message));
            return computedHas;
        }

        public static string GetHash256Str(string message)
        {
            byte[] hashed = GetHash256(message);
            string result = BitConverter.ToString(hashed).Replace("-", "");
            return result;
        }

        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            Console.WriteLine("SHA256 : " + GetHash256Str(message));
        }
    }
}