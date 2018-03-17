using System;
using System.Security.Cryptography;
using System.Text;

namespace DaneChain.Core
{
    public static class Extensions
    {
        public static long ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalMilliseconds);
        }

        public static string GetDifficultyString(this int difficulty) => 
            new string(new char[difficulty]).Replace('\0','0');
    }

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
    }
}
