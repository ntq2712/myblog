

using System.Security.Cryptography;
using System.Text;

namespace blog.Helper
{
    public class ApiKeyProvider
    {
        private static readonly string SecretKey = "$grow-family-team-2025$";

        public static string GetSecretKey() => SecretKey;

        public static string Decrypt(string encryptedText)
        {
            byte[] decodedBytes = Convert.FromBase64String(encryptedText);
            return Encoding.UTF8.GetString(decodedBytes);
        }

        public static bool IsValidBase64(string base64String)
        {
            Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
            return Convert.TryFromBase64String(base64String, buffer, out _);
        }
    }
}