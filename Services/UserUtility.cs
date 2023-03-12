using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace UserService.Services
{
    public class UserUtility : IUserUtility
    {
        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            // if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", nameof(storedHash));
            // if (storedSalt.Length != 128 / 8) throw new ArgumentException("Invalid length of password salt (16 bytes expected).", nameof(storedSalt));

            byte[] hash = KeyDerivation.Pbkdf2(
                password: password,
                salt: storedSalt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 10000,
                numBytesRequested: 256 / 8);

            return storedHash.SequenceEqual(hash);
        }
    }
}