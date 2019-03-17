using System;

namespace PZProject.Handlers.Utils
{
    public static class PasswordHashHandler
    {
        private const int ExpectedHashLength = 64;
        private const int ExpectedSaltLength = 128;

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            AssertThatPasswordIsNotEmpty(password);

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            AssertThatPasswordIsNotEmpty(storedHash, storedSalt);

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false;
                }
            }

            return true;
        }

        private static void AssertThatPasswordIsNotEmpty(byte[] storedHash, byte[] storedSalt)
        {
            if (storedHash.Length != ExpectedHashLength) throw new ArgumentException($"Invalid length of password hash ({ExpectedHashLength} bytes expected).");
            if (storedSalt.Length != ExpectedSaltLength) throw new ArgumentException($"Invalid length of password salt ({ExpectedSaltLength} bytes expected).");
        }

        private static void AssertThatPasswordIsNotEmpty(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Password cannot be empty or whitespace only string.");
        }
    }
}