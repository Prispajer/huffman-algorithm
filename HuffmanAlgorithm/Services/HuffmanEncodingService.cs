using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanEncodingService : IHuffmanEncodingService
    {
        // Method to calculate the frequency of characters in the input text
        // This method returns a dictionary where the key is the character and the value is its frequency
        public Dictionary<char, int> CalculateOccurrenceFrequency(string inputText)
        {
            // Group the characters by their value and count the occurrences of each character
            return inputText.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        // Method to calculate the frequency of bytes in the input binary data
        // This method returns a dictionary where the key is the byte and the value is its frequency
        public Dictionary<byte, int> CalculateOccurrenceFrequency(byte[] inputData)
        {
            // Group the bytes by their value and count the occurrences of each byte
            return inputData.GroupBy(b => b).ToDictionary(g => g.Key, g => g.Count());
        }

        // Method to generate a priority queue based on the frequency of characters
        // The priority queue stores nodes of the Huffman tree where the key is the frequency
        // and the value is the node containing the character and its frequency
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary)
        {
            // Create a priority queue to hold Huffman nodes, sorted by frequency
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));
            // Enqueue each character node with its corresponding frequency
            foreach (var entry in frequencyDictionary)
            {
                priorityQueue.Enqueue(new HuffmanNode { Symbol = entry.Key, Frequency = entry.Value }, entry.Value);
            }
            return priorityQueue;
        }

        // Method to generate a priority queue based on the frequency of bytes
        // Similar to the above method, but this works with bytes instead of characters
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<byte, int> frequencyDictionary)
        {
            // Create a priority queue to hold Huffman nodes, sorted by frequency
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));
            // Enqueue each byte node with its corresponding frequency
            foreach (var entry in frequencyDictionary)
            {
                // Since the symbol is a byte, it needs to be cast to char before storing
                priorityQueue.Enqueue(new HuffmanNode { Symbol = (char)entry.Key, Frequency = entry.Value }, entry.Value);
            }
            return priorityQueue;
        }
    }
}
