using System.Text;
using System.Security.Cryptography;

namespace CarRentalSystem.Web.Extensions
{
    public static class PasswordExtensions
    {
        public static string HashPassword(this string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        public static bool VerifyPassword(this string enteredPassword, string storedPassword)
        {
            var enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPassword;
        }
    }
}