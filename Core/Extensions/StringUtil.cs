using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

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

        //https://stackoverflow.com/questions/8830510/c-sharp-sign-data-with-rsa-using-bouncycastle
        public static byte[] ApplyECDSASig(this ICipherParameters privateKey, string input)
        {
            var sig = SignerUtilities.GetSigner("ECDSA");
            sig.Init(true, privateKey);
            var data = Encoding.UTF8.GetBytes(input);
            sig.BlockUpdate(data, 0, data.Length);
            return sig.GenerateSignature();
        }

        public static bool VerifyECDSASig(this ICipherParameters publicKey, 
            string data,
            byte[] signature)
        {
            var signer = SignerUtilities.GetSigner("ECDSA");
            signer.Init(false, publicKey);
            var msgBytes = Encoding.UTF8.GetBytes(data);
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
            return signer.VerifySignature(signature);
        }
    }
}