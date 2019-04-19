using System;
using System.Security.Cryptography;
using System.Text;

namespace CryptoUtilsConsole.hashing
{
    class HmacSHA1Demos
    {
        
  
        public static void LaunchDemo()
        {
            string message = "The quick brown fox jumps over the lazy dog";
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
    }
}