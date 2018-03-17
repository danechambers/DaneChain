using System;

namespace myblockchain
{
    public class Block
    {
        public string Hash { get; private set; }
        public string PreviousHash { get; }
        private readonly string data;
        private readonly long timeStamp;
        public int Nonce { get; private set; } = 0;

        public Block(string data, string previousHash)
        {
            this.data = data;
            PreviousHash = previousHash;
            timeStamp = DateTime.Now.ToUnixTime();
            Hash = CalculateHash();
        }

        public string CalculateHash() => 
            StringUtil.ApplySha256(PreviousHash + 
                timeStamp.ToString() + 
                Nonce.ToString() +
                data);

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