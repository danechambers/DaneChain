using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace DaneChain.Core
{
    public class Wallet
    {
        public ECKeyParameters PublicKey { get; private set; }
        public ECKeyParameters PrivateKey { get; private set; }

        public Wallet()
        {
            GenerateKeyPair();
        }
        
        private void GenerateKeyPair()
        {
            var keyGen = new ECKeyPairGenerator("ECDSA");
            var secureRandom = SecureRandom.GetInstance("SHA1PRNG");
            var ecSpec = X9ObjectIdentifiers.Prime192v1;
            keyGen.Init(new ECKeyGenerationParameters(ecSpec, secureRandom));
            var keyPair = keyGen.GenerateKeyPair();
            PublicKey = keyPair.Public as ECKeyParameters;
            PrivateKey = keyPair.Private as ECKeyParameters;
        }
    }
}