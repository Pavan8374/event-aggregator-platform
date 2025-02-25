using Identity.Domain.Common;

namespace Identity.Domain.ValueObjects.Users
{
    public class Password : ValueObject
    {
        public string Hash { get; private set; }
        public string Salt { get; private set; }

        private Password(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public static Password Create(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                throw new DomainException("Password cannot be empty");

            ValidatePassword(plainPassword);

            // Generate a random salt
            var salt = GenerateSalt();

            // Hash the password with the salt
            var hash = HashPassword(plainPassword, salt);

            return new Password(hash, salt);
        }

        public static Password CreateFromExisting(string hash, string salt)
        {
            if (string.IsNullOrWhiteSpace(hash))
                throw new DomainException("Hash cannot be empty");

            if (string.IsNullOrWhiteSpace(salt))
                throw new DomainException("Salt cannot be empty");

            return new Password(hash, salt);
        }

        public bool VerifyPassword(string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(plainPassword))
                return false;

            var hashToCompare = HashPassword(plainPassword, Salt);
            return Hash == hashToCompare;
        }

        private static void ValidatePassword(string password)
        {
            // Password policy validation
            if (password.Length < 8)
                throw new DomainException("Password must be at least 8 characters long");

            if (!password.Any(char.IsUpper))
                throw new DomainException("Password must contain at least one uppercase letter");

            if (!password.Any(char.IsLower))
                throw new DomainException("Password must contain at least one lowercase letter");

            if (!password.Any(char.IsDigit))
                throw new DomainException("Password must contain at least one number");

            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
                throw new DomainException("Password must contain at least one special character");
        }

        private static string GenerateSalt()
        {
            byte[] saltBytes = new byte[32]; // 256 bits
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private static string HashPassword(string password, string salt)
        {
            using (var pbkdf2 = new System.Security.Cryptography.Rfc2898DeriveBytes(
                password,
                Convert.FromBase64String(salt),
                10000,  // Number of iterations
                System.Security.Cryptography.HashAlgorithmName.SHA256))
            {
                byte[] hashBytes = pbkdf2.GetBytes(32); // 256 bits
                return Convert.ToBase64String(hashBytes);
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Hash;
            yield return Salt;
        }
    }
}
