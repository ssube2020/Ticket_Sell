using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Ticket_Sell.Helpers
{

    public static class PasswordHasher
    {
        public static byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            Random rnd = new Random();
            for (int i=0; i<salt.Length;i++)
            {
                salt[i] = (byte)rnd.Next(1, 10);
            }
            return salt;
        }

        public static string ConvertToString(byte[] data)
        {
            string toreturn = "";
            for (int i = 0; i < data.Length; i++)
            {
                toreturn += data[i];
            }
            return toreturn;
        }

        public static byte[] ConvertToBytes(string data)
        {
            byte[] salt = new byte[128/8];
            for (int i = 0; i < data.Length; i++)
            {
                string c = data[i].ToString();
                int k = Convert.ToInt32(c);
                salt[i] = (byte)k;
            }
            return salt;
        }

        public static string HashPassword(string password, byte[] GeneratedSalt)
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: GeneratedSalt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
