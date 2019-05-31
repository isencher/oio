using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ting.lib
{
    public class withSecure
    {
        //private static sec sec = new sec();

        /// <summary>
        /// to encrypt a string
        /// </summary>
        /// <param name="plainText">plain text</param>
        /// <param name="password">password</param>
        /// <returns>a encrypted string</returns>
        public static string Encrypt(
          string plainText, string password)
        {
            return sec.Encrypt(plainText, password);
        }

        /// <summary>
        /// to decrypt a string
        /// </summary>
        /// <param name="cryptoText">encrypted string</param>
        /// <param name="password">password</param>
        /// <returns>a decrypted string</returns>
        public static string Decrypt(
          string cryptoText, string password)
        {
            return sec.Decrypt(cryptoText, password);
        }

        /// <summary>
        /// to generate a salt string
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            // generate a random salt 
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            return saltText;
        }

        /// <summary>
        /// to generate a salted and hashed password string
        /// </summary>
        /// <param name="saltText">salt string</param>
        /// <param name="password">password</param>
        /// <returns>a salted and hashed password string</returns>
        public static string GenerateSaltedHashedPassword(string saltText, string password)
        {
            // generate the salted and hashed password 
            var sha = SHA256.Create();
            var saltedPassword = password + saltText;
            var saltedhashedPassword = Convert.ToBase64String(
              sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
            return saltedhashedPassword;
        }

        /// <summary>
        /// to check whether a password is match
        /// </summary>
        /// <param name="userSaltText"></param>
        /// <param name="userSaltedHashedPassword"></param>
        /// <param name="password"></param>
        /// <returns>pass or not</returns>
        public static bool CheckPassword(string userSaltText, string userSaltedHashedPassword, string password)
        {
            // re-generate the salted and hashed password 
            var sha = SHA256.Create();
            var saltedPassword = password + userSaltText;
            var saltedhashedPassword = Convert.ToBase64String(
              sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));

            return (saltedhashedPassword == userSaltedHashedPassword);
        }
    }
}
