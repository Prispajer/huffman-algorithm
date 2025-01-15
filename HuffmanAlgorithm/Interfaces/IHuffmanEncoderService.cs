using HuffmanAlgorithm.Models;
using System.Collections.Generic;
using System.Text;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanEncoderService
    {
        Dictionary<char, string> GenerateHuffmanCodes(string inputText);
        Dictionary<char, int> CalculateOccurrenceFrequency(string inputText);
        PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary);
        HuffmanNode GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue);
        void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codes);
        string GenerateDot(HuffmanNode node);
        void GenerateDotRecursive(HuffmanNode node, StringBuilder dotBuilder);
        string DecodeHuffmanData(string encodedData, HuffmanNode root);
    }
}
