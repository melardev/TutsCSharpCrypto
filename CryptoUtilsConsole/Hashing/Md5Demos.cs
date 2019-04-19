using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtilsConsole.hashing
{
    class Md5Demos
    {
        public static string LaunchMd5_StringBuilder(string str)
        {
            MD5 hash = MD5.Create();
            byte[] h = hash.ComputeHash(Encoding.Unicode.GetBytes(str));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in h)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string GetMd5_BitConverter(string message)
        {
            using (var md5 = MD5.Create())
            {
                byte[] hashmeHashed = md5.ComputeHash(Encoding.UTF8.GetBytes(message));
                string result = BitConverter.ToString(hashmeHashed).Replace("-", "");
                return result;
            }
        }

        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
            Console.WriteLine("MD5 : " + GetMd5_BitConverter(message));
            Console.WriteLine("MD5 : " + LaunchMd5_StringBuilder(message));
        }
    }
}