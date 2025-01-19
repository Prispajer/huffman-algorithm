using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanTreeBuilderService
    {
        HuffmanNode GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue);
        void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codes);
    }
}
