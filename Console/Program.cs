using System;
using System.Collections.Generic;
using System.Linq;
using DaneChain.Core;
using Newtonsoft.Json;

namespace DaneChain.ConsoleUI
{
    class Program
    {
        static List<Block> blockChain = new List<Block>();

        static int difficulty = 5;

        static void Main(string[] args)
        {
            Console.WriteLine("Trying to mine block 1...");
            AddBlock(new Block("Hi im the first block", "0"));

            Console.WriteLine("Trying to mine block 2...");
            AddBlock(new Block("Yo im the second block", blockChain.Last().Hash));

            Console.WriteLine("Trying to mine block 3...");
            AddBlock(new Block("Hey im the third block", blockChain.Last().Hash));

            Console.WriteLine($"Is Blockchain valid: {IsChainValid()}");
            
            Console.WriteLine(JsonConvert.SerializeObject(blockChain, Formatting.Indented));
        }

        static bool IsChainValid()
        {
            Block currentBlock;
            Block previousBlock;

            string hashTarget = difficulty.GetDifficultyString();

            for(int i = 1; i < blockChain.Count; i++)
            {
                currentBlock = blockChain[i];
                previousBlock = blockChain[i-1];

                if(!currentBlock.Hash.Equals(currentBlock.CalculateHash()))
                {
                    Console.WriteLine("Current hashe are not equal.");
                    return false;
                }

                if(!previousBlock.Hash.Equals(currentBlock.PreviousHash))
                {
                    Console.WriteLine("Previous hashes are not equal.");
                    return false;
                }

                if(!currentBlock.Hash.Substring(0, difficulty).Equals(hashTarget))
                {
                    Console.WriteLine("This block hasn't been mined.");
                    return false;
                }
            }

            return true;
        }

        static void AddBlock(Block newBlock)
        {
            newBlock.MineBlock(difficulty);
            blockChain.Add(newBlock);
        }
    }
}
