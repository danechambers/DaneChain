using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Parameters;

namespace DaneChain.Core.Extensions
{
    public static class StringUtil
    {
        public static string ApplySha256(this string input)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;

            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }

            return hashString;
        }

        public static string GetStringFromKey(this ECKeyParameters key) => 
            Convert.ToBase64String(key.PublicKeyParamSet.GetEncoded());
    }
}