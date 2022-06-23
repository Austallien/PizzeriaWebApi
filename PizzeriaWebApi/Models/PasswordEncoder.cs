using System;
using System.Security.Cryptography;
using System.Text;

namespace Api.Models
{
    public class PasswordCryptograph
    {
        public enum Mode
        {
            Encrypt,
            Decrypt
        }

        public static string Encrypt(string Password)
        {
            string encryptedPasword = Cryptor(Mode.Encrypt, Password);
            return encryptedPasword;
        }

        private static string Cryptor(Mode Mode, string Password)
        {
            RijndaelManaged crypt = GetCrypt();

            ICryptoTransform transform = Mode is Mode.Encrypt ? crypt.CreateEncryptor() : crypt.CreateDecryptor();

            byte[] bytePassword = Encoding.UTF32.GetBytes(Password);
            byte[] encryptedPassword = transform.TransformFinalBlock(bytePassword, 0, bytePassword.Length);

            string result = Convert.ToBase64String(encryptedPassword);
            return result;
        }

        private static RijndaelManaged GetCrypt()
        {
            RijndaelManaged crypt = new RijndaelManaged();
            crypt.KeySize = 256;
            crypt.BlockSize = 128;
            crypt.Padding = PaddingMode.PKCS7;
            crypt.Mode = CipherMode.ECB;
            crypt.Key = Encoding.UTF8.GetBytes("KpZHICsvHOvKVLztBR1Oo8os2VzcnxMT");

            return crypt;
        }
    }
}