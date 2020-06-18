using System;
using System.Security.Cryptography;
using System.Text;
using System.Security;

namespace PicturesUploader.FTPConnection
{
    internal static class Cryptography
    {
        private static byte[] entropy;// = System.Text.Encoding.Unicode.GetBytes("Salt Is Not A Password");
        static Cryptography()
        {
            entropy = GetEntropy();
        }

        private static byte[] GetEntropy()
        {
            string salt = Environment.UserName;
            MD5CryptoServiceProvider csp = new MD5CryptoServiceProvider();
            byte[] byteValue = Encoding.Unicode.GetBytes(salt);
            byte[] byteHash = csp.ComputeHash(byteValue);
            return byteHash;
        }
        private static string EncryptString(System.Security.SecureString input)
        {
            byte[] encryptedData = System.Security.Cryptography.ProtectedData.Protect(
                System.Text.Encoding.Unicode.GetBytes(ToInsecureString(input)),
                entropy,
                System.Security.Cryptography.DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }
        public static string EncryptString(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return EncryptString(ToSecureString(input));
        }
        public static string DecryptString(string encryptedData)
        {
            if (string.IsNullOrEmpty(encryptedData))
            {
                return string.Empty;
            }
            return ToInsecureString(DecryptToSecureString(encryptedData));
        }
        private static SecureString DecryptToSecureString(string encryptedData)
        {
            try
            {
                byte[] decryptedData = System.Security.Cryptography.ProtectedData.Unprotect(
                    Convert.FromBase64String(encryptedData),
                    entropy,
                    System.Security.Cryptography.DataProtectionScope.CurrentUser);
                return ToSecureString(System.Text.Encoding.Unicode.GetString(decryptedData));
            }
            catch
            {
                return new SecureString();
            }
        }

        private static SecureString ToSecureString(string input)
        {
            SecureString secure = new SecureString();
            foreach (char c in input)
            {
                secure.AppendChar(c);
            }
            secure.MakeReadOnly();
            return secure;
        }
        private static string ToInsecureString(SecureString input)
        {
            string returnValue = string.Empty;
            IntPtr ptr = System.Runtime.InteropServices.Marshal.SecureStringToBSTR(input);
            try
            {
                returnValue = System.Runtime.InteropServices.Marshal.PtrToStringBSTR(ptr);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeBSTR(ptr);
            }
            return returnValue;
        }

    }
}
