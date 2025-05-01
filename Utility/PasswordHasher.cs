using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace TaskManagerAPI.Utility {
    public class Passwordhasher {
        public static string HashPassword(string password) {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create()) {
                rng.GetBytes(salt);
                }

            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
            }

        public static bool VerifyPassword(string enteredPassword, string storedPassword) {
            if (enteredPassword == storedPassword) return true;
            var parts = storedPassword.Split(':');
            if (parts.Length != 2)
                return false;

            var salt = Convert.FromBase64String(parts[0]);
            var storedHash = parts[1];

            var enteredHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: enteredPassword,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return enteredHash == storedHash;
            }
        }
    }
