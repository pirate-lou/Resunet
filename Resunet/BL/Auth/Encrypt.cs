using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Resunet.BL.Auth
{
    public class Encrypt : IEncrypt
    {
        public Encrypt()
        {
        }

        public string HashPassword(string password, string salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password,
                System.Text.Encoding.ASCII.GetBytes(salt),
                KeyDerivationPrf.HMACSHA512, // 512 делим на бит (512/8=64)
                5000,
                64 // поэтому берем 64
            ));
        }
    }
}