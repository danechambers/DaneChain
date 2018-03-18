using System;
using DaneChain.Core.Extensions;

namespace DaneChain.Core
{
    public class Block
    {
        public string Hash { get; private set; }
        public string PreviousHash { get; }
        public string Data { get; }
        private readonly long timeStamp;
        public int Nonce { get; private set; } = 0;

        public Block(string data, string previousHash)
        {
            Data = data;
            PreviousHash = previousHash;
            timeStamp = DateTime.Now.ToUnixTime();
            Hash = CalculateHash();
        }

        public string CalculateHash() => 
            StringUtil.ApplySha256(PreviousHash + 
                timeStamp.ToString() + 
                Nonce.ToString() +
                Data);

        public void MineBlock(int difficulty)
        {
            string target = difficulty.GetDifficultyString();
            while(!Hash.Substring(0, difficulty).Equals(target))
            {
                Nonce++;
                Hash = CalculateHash();
            }
            Console.WriteLine($"Block Mined!! : {Hash}" );
        }
    }
}