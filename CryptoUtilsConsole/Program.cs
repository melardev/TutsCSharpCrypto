using CryptoUtilsConsole.AsymmetricCrypto;
using CryptoUtilsConsole.hashing;
using CryptoUtilsConsole.symmetric_crypto;

namespace CryptoUtilsConsole
{
    public enum CryptoOperation
    {
        ENCRYPT,
        DECRYPT
    };

    class Program
    {
        static void Main(string[] args)
        {
            // WARNING
            // the below code is only meant to indicate you where the snippets you are looking
            // for are, if you run this code as is, the app crashes because it will not find a the hardcoded
            // file path to encrypt Hashing

            HmacMD5Demos.LaunchDemo();
            HmacSHA1Demos.LaunchDemo();
            Md5Demos.LaunchDemo();
            Sha1Demos.LaunchDemo();
            Sha256Demos.LaunchDemo();
            Sha512Demos.LaunchDemo();

            // Symmetric crypto
            AESDemos.LaunchDemo();
            DESDemos.LaunchDemo();
            DESDemos.LaunchFileDemo();
            TripleDESDemos.LaunchDemo();

            // Asymmetric crypto
            RSACspDemo.LaunchDemo();
            RsaFileDemo.LaunchDemo();
            RsaInMemoryDemo.LaunchDemo();
        }
    }
}