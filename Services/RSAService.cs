using System.Security.Cryptography;
using System.Text;
using blog.Repository;

namespace blog.Services
{
    public class RSAService : IRSA
    {
        private RSA rSA;
        private static readonly string _privateKeyPath = "Keys/private.pem";
        private static readonly string _publicKeyPath = "Keys/public.pem";

        public RSAService()
        {
            rSA = RSA.Create();
            if (!File.Exists(_privateKeyPath) || !File.Exists(_publicKeyPath))
            {
                GenerateRSAKeys();
            }
            else
            {
                LoadRSAKeys();
            }
        }

        private void GenerateRSAKeys()
        {
            string plk = Convert.ToBase64String(rSA.ExportRSAPublicKey());
            string pk = Convert.ToBase64String(rSA.ExportRSAPrivateKey());

            Directory.CreateDirectory("Keys");
            File.WriteAllText(_privateKeyPath, pk);
            File.WriteAllText(_publicKeyPath, plk);
        }

        private void LoadRSAKeys()
        {
            string privateKey = File.ReadAllText(_privateKeyPath);
            rSA.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
        }
        public string GetPublicKey()
        {

            return File.ReadAllText(_publicKeyPath);
        }

        public string Decrypt(string encryptedText)
        {
            try
            {

                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

                byte[] decryptedBytes = rSA.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
            catch (CryptographicException ex)
            {
                Console.WriteLine($"❌ RSA Decryption Failed: {ex.Message}");
                throw;
            }
        }
    }
}