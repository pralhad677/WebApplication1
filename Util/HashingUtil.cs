using System.Text;
using System.Security.Cryptography;
namespace WebApplication1.Util
{
    public   class HashingUtil
    {
        public    byte[]    HashPassword(string password)
        {
            byte[] salt = new byte[16];
             new Random().NextBytes(salt); // Use a cryptographically secure random number generator (CSPRNG)

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + Convert.ToBase64String(salt)); // Append salt to password
            return SHA256.Create().ComputeHash(passwordBytes);
        }
    }
}
