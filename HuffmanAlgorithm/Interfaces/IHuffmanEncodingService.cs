using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanEncodingService
    {
        Dictionary<char, int> CalculateOccurrenceFrequency(string inputText);
        PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary);
    }
}
