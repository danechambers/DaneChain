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
        public ECKeyParameters Sender { get; } // senders address/public key.
        public ECKeyParameters Recipient { get; } // Recipients address/public key.
        public float Value { get; }
        public byte[] Signature { get; }

        // this is to prevent anybody else from spending funds in our wallet.
        public List<TransactionInput> Inputs { get; } = new List<TransactionInput>();

        private static int sequence = 0;

        public Transaction(
            ECKeyParameters from,
            ECKeyParameters to,
            float value,
            List<TransactionInput> inputs
        )
        {
            if(!(Sender is ECPublicKeyParameters senderKey) ||
                !(Recipient is ECPublicKeyParameters recipientKey))
                throw new ArgumentException(
                    "Both sender key and recipient keys need to be public keys.");

            Sender = from;
            Recipient = to;
            Value = value;
            Inputs = inputs;
        }

        // This Calculates the transaction hash (which will be used as its Id)
        private string CalculateHash()
        {
            sequence++;
            return StringUtil.ApplySha256(
                Sender.GetStringFromKey() +
                Recipient.GetStringFromKey() +
                Value.ToString() + sequence.ToString()
            );
        }

    }
}