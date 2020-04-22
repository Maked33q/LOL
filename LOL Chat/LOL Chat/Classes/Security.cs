using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace MainFunc.Classes
{
    class Security
    {
        internal static CngKey userKeySignature;
        internal static byte[] userPublicKeyBlop;

        static void CreateKeys() 
        {
            userKeySignature = CngKey.Create(CngAlgorithm.ECDsaP256);
            userPublicKeyBlop = userKeySignature.Export(CngKeyBlobFormat.GenericPublicBlob);
        }
        static byte[] CreateSignature(byte[] data, CngKey key) 
        {
            byte[] signature;
            var signingAlg = new ECDsaCng(key);
            signature = signingAlg.SignData(data);
            signingAlg.Clear();
            return signature;
        }
        static bool VerifySignature(byte[] data,byte[] signature, byte[] publicKey) 
        {
            bool value = false;
            using (CngKey key = CngKey.Import(publicKey, CngKeyBlobFormat.GenericPublicBlob)) 
            {
                var signingAlg = new ECDsaCng(key);
                value = signingAlg.VerifyData(data, signature);
                signingAlg.Clear();
            }
            return value;
        }

        static void Test() 
        {
            CreateKeys();

            byte[] userData = Encoding.UTF8.GetBytes("Alice");
            byte[] userSignature = CreateSignature(userData, userKeySignature);
            Console.WriteLine("User created signature:  {0}",
                  Convert.ToBase64String(userSignature));

            if (VerifySignature(userData, userSignature, userPublicKeyBlop))
            {
                Console.WriteLine("Сигнатуры совпадают. Подпись была успешно проверена.");
            }
        }




    }
}
