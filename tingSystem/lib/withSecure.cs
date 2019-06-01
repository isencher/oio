using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace ting.lib
{
    public class withSecure
    {
        //private static sec sec = new sec();

        public static string Encrypt(
          string plainText, string password="4398Qez@")
        {
            return sec.Encrypt(plainText, password);
        }

        public static string Decrypt(
          string cryptoText, string password)
        {
            return sec.Decrypt(cryptoText, password);
        }

        public static string GenerateSalt()
        {
            // generate a random salt 
            var rng = RandomNumberGenerator.Create();
            var saltBytes = new byte[16];
            rng.GetBytes(saltBytes);
            var saltText = Convert.ToBase64String(saltBytes);
            return saltText;
        }

        public static string GenerateSaltedHashedPassword(string saltText, string password)
        {
            // generate the salted and hashed password 
            var sha = SHA256.Create();
            var saltedPassword = password + saltText;
            var saltedhashedPassword = Convert.ToBase64String(
              sha.ComputeHash(Encoding.Unicode.GetBytes(saltedPassword)));
            return saltedhashedPassword;            
        }

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
