

namespace api.Utils
{
    using System.Security.Cryptography;
    using Microsoft.AspNetCore.Cryptography.KeyDerivation;

    public static class PasswordHasher
    {
        public static (string Hash, string Salt) HashPassword(string password)
        {
            // Generate a salt
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Generate the hash
            string saltString = Convert.ToBase64String(salt);
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return (hash, saltString);
        }

        public static bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Convert the stored salt back to bytes
            byte[] salt = Convert.FromBase64String(storedSalt);

            // Hash the input password with the stored salt
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            // Compare the generated hash with the stored hash
            return hash == storedHash;
        }
    }

}