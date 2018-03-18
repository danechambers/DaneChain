using System;

namespace DaneChain.Core.Extensions
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
}
