using System;
using System.Collections.Generic;
using DaneChain.Core.Extensions;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;

namespace DaneChain.Core
{
    public class Transaction
    {
        public string TransactionId { get; private set; } // this is also the hash of the transaction.
        public AsymmetricKeyParameter Sender { get; } // senders address/public key.
        public AsymmetricKeyParameter Recipient { get; } // Recipients address/public key.
        public float Value { get; }
        public byte[] Signature { get; }

        // this is to prevent anybody else from spending funds in our wallet.
        public List<TransactionInput> Inputs { get; } = new List<TransactionInput>();

        private static int sequence = 0;

        public Transaction(
            AsymmetricKeyParameter from,
            AsymmetricKeyParameter to,
            float value,
            List<TransactionInput> inputs
        )
        {
            Sender = from;
            Recipient = to;
            Value = value;
            Inputs = inputs;
        }

        // This Calculates the transaction hash (which will be used as its Id)
        private string CalculateHash()
        {
            sequence++;
            if(!(Sender is ECPublicKeyParameters senderKey) ||
                !(Recipient is ECPublicKeyParameters recipientKey))
                throw new InvalidCastException(
                    "Unable to get ECPublicKeyParam from generic AsymmetricKeyParam");
            return StringUtil.ApplySha256(
                senderKey.GetStringFromKey() +
                recipientKey.GetStringFromKey() +
                Value.ToString() + sequence.ToString()
            );
        }

    }
}