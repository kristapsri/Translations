using Konscious.Security.Cryptography;

namespace TranslationsAdmin.Services
{
    public interface IPasswordHashService
    {
        public string HashPassword(string password);
    }

    public class PasswordHashService : IPasswordHashService
    {
        private readonly byte[] _salt;

        public PasswordHashService(IConfiguration configuration)
        {
            _salt = GetSaltFromConfig(configuration);
        }

        public string HashPassword(string password)
        {
            byte[] saltedPasswordBytes = ConcatenatePasswordAndSalt(password, _salt);

            using (var argon2 = new Argon2id(saltedPasswordBytes))
            {
                argon2.Salt = _salt;
                argon2.Iterations = 2;
                argon2.MemorySize = 1024; //kb
                argon2.DegreeOfParallelism = 4;

                byte[] hashedBytes = argon2.GetBytes(16);

                return Convert.ToBase64String(hashedBytes);
            }
        }

        private byte[] ConcatenatePasswordAndSalt(string password, byte[] salt)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] saltedPasswordBytes = new byte[passwordBytes.Length + salt.Length];
            passwordBytes.CopyTo(saltedPasswordBytes, 0);
            salt.CopyTo(saltedPasswordBytes, passwordBytes.Length);
            return saltedPasswordBytes;
        }

        private byte[] GetSaltFromConfig(IConfiguration configuration)
        {
            string saltValue = configuration["Salt"];
            byte[] salt = Convert.FromBase64String(saltValue);
            return salt;
        }
    }
}
