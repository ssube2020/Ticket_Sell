using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ticket_Sell.Helpers
{

    public static class HashPassword
    {
        public static byte[] salt = new byte[128/8];
        public static char[] characters = salt.Select(b => (char)b).ToArray();
        public static string saltstring =  new string (characters);
        public static string GetPassHash(string password, byte[] salt)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            //Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            
            return hashed;
        }

        //public static string GetSalt()
        //{
        //    char[] characters = salt.Select(b => (char)b).ToArray();
        //    return new string(characters);
        //}
    }
}
