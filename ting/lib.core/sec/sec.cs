using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace ting.lib
{
    internal static class sec
    {
        // salt size must be at least 8 bytes, we will use 16 bytes 
        private static readonly byte[] salt =
          Encoding.Unicode.GetBytes("7BANANAS");

        // iterations must be at least 1000, we will use 2000 
        private static readonly int iterations = 2000;

        /// <summary>
        /// to encrypt a string
        /// </summary>
        /// <param name="plainText">plain text</param>
        /// <param name="password">password</param>
        /// <returns>encrypted string</returns>
        internal static string Encrypt(
          string plainText, string password)
        {
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(
              password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32); // set a 256-bit key 
            aes.IV = pbkdf2.GetBytes(16); // set a 128-bit IV 
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(
              ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(plainBytes, 0, plainBytes.Length);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// to decrypt a string
        /// </summary>
        /// <param name="cryptoText">encrypted string</param>
        /// <param name="password">password</param>
        /// <returns>decrypted string/plain text</returns>
        internal static string Decrypt(
          string cryptoText, string password)
        {
            byte[] cryptoBytes = Convert.FromBase64String(cryptoText);
            var aes = Aes.Create();
            var pbkdf2 = new Rfc2898DeriveBytes(
              password, salt, iterations);
            aes.Key = pbkdf2.GetBytes(32);
            aes.IV = pbkdf2.GetBytes(16);
            var ms = new MemoryStream();
            using (var cs = new CryptoStream(
              ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
            {
                cs.Write(cryptoBytes, 0, cryptoBytes.Length);
            }
            return Encoding.Unicode.GetString(ms.ToArray());
        }

        #region collect codes
        //public static string PublicKey;


        //public static string ToXmlStringExt(
        //    this RSA rsa, bool includePrivateParameters)
        //{
        //    var p = rsa.ExportParameters(includePrivateParameters);
        //    XElement xml;
        //    if (includePrivateParameters)
        //    {
        //        xml = new XElement("RSAKeyValue"
        //                      , new XElement("Modulus", Convert.ToBase64String(p.Modulus))
        //                      , new XElement("Exponent",
        //                        Convert.ToBase64String(p.Exponent))
        //                      , new XElement("P", Convert.ToBase64String(p.P))
        //                      , new XElement("Q", Convert.ToBase64String(p.Q))
        //                      , new XElement("DP", Convert.ToBase64String(p.DP))
        //                      , new XElement("DQ", Convert.ToBase64String(p.DQ))
        //                      , new XElement("InverseQ",
        //                        Convert.ToBase64String(p.InverseQ))
        //        );
        //    }
        //    else
        //    {
        //        xml = new XElement("RSAKeyValue"
        //                      , new XElement("Modulus", Convert.ToBase64String(p.Modulus))
        //                      , new XElement("Exponent",
        //                        Convert.ToBase64String(p.Exponent))
        //        );
        //    }
        //    return xml?.ToString();
        //}

        //public static void FromXmlStringExt(
        //  this RSA rsa, string parametersAsXml)
        //{
        //    var xml = XDocument.Parse(parametersAsXml);
        //    var root = xml.Element("RSAKeyValue");
        //    var p = new RSAParameters
        //    {
        //        Modulus = Convert.FromBase64String(
        //        root.Element("Modulus").Value),
        //        Exponent = Convert.FromBase64String(
        //        root.Element("Exponent").Value)
        //    };
        //    if (root.Element("P") != null)
        //    {
        //        p.P = Convert.FromBase64String(root.Element("P").Value);
        //        p.Q = Convert.FromBase64String(root.Element("Q").Value);
        //        p.DP = Convert.FromBase64String(root.Element("DP").Value);
        //        p.DQ = Convert.FromBase64String(root.Element("DQ").Value);
        //        p.InverseQ = Convert.FromBase64String(
        //          root.Element("InverseQ").Value);
        //    }
        //    rsa.ImportParameters(p);
        //}

        //public static string GenerateSignature(string data)
        //{
        //    byte[] dataBytes = Encoding.Unicode.GetBytes(data);
        //    var sha = SHA256.Create();
        //    var hashedData = sha.ComputeHash(dataBytes);

        //    var rsa = RSA.Create();
        //    PublicKey = rsa.ToXmlStringExt(false); // exclude private key 

        //    return Convert.ToBase64String(rsa.SignHash(hashedData,
        //      HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1));
        //}

        //public static bool ValidateSignature(
        //  string data, string signature)
        //{
        //    byte[] dataBytes = Encoding.Unicode.GetBytes(data);
        //    var sha = SHA256.Create();
        //    var hashedData = sha.ComputeHash(dataBytes);

        //    byte[] signatureBytes = Convert.FromBase64String(signature);

        //    var rsa = RSA.Create();
        //    rsa.FromXmlStringExt(PublicKey);

        //    return rsa.VerifyHash(hashedData, signatureBytes,
        //      HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        //}

        //public static void GetRandomNumbers()
        //{
        //    var r = new Random(Seed: 5765);
        //    int dieRoll = r.Next(minValue: 1, maxValue: 7); // returns 1 to 6
        //    double real = r.NextDouble(); // returns 0.0 to 1.0
        //    var arrayOfBytes = new byte[256];
        //    r.NextBytes(arrayOfBytes); // 256 random bytes in array
        //}

        //public static byte[] GetRandomKeyOrIV(int size)
        //{
        //    var r = RandomNumberGenerator.Create();
        //    var data = new byte[size];
        //    r.GetNonZeroBytes(data);
        //    return data;
        //}
        #endregion


    }
}
