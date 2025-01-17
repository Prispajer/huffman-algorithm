using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanEncodingService
    {
        Dictionary<char, int> CalculateOccurrenceFrequency(string inputText);
        Dictionary<byte, int> CalculateOccurrenceFrequency(byte[] inputData);
        PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary);
        PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<byte, int> frequencyDictionary);
    }
}
