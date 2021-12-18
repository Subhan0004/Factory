using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Factory.Core
{
    public static class SecurityHelper
    {
        public static string ComputeSha256Hash(string rawDate)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawDate));

                StringBuilder builder = new StringBuilder();

                foreach (var t in bytes)
                {
                    builder.Append(t.ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public static string Decrypt(string cipherText)
        {
            if (cipherText == null)
                return null;

            byte[] cipher = Convert.FromBase64String(cipherText);
            byte[] plain;
            byte[] iv = new byte[16];

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;            // in bits
                aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV

                plain = iv;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipher, 0, cipher.Length);
                    }

                    plain = ms.ToArray();
                }

                string plainText = Encoding.UTF8.GetString(plain);
                return plainText;
            }
        }

        public static string Encrypt(string plainText)
        {
            if (plainText == null)
                return null;

            byte[] cipher;
            byte[] plain = Encoding.UTF8.GetBytes(plainText);

            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 128;          // in bits
                aes.Key = new byte[128 / 8];  // 16 bytes for 128 bit encryption
                aes.IV = new byte[128 / 8];   // AES needs a 16-byte IV

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plain, 0, plain.Length);
                    }

                    cipher = ms.ToArray();
                }

                string cipherText = Convert.ToBase64String(cipher);
                return cipherText;
            }
        }
    }
}
