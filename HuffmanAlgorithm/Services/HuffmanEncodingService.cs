using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanEncodingService : IHuffmanEncodingService
    {
        // Method to calculate the frequency of characters in the input text.
        // Returns a dictionary where the key is the character, and the value is its frequency in the input text.
        public Dictionary<char, int> CalculateOccurrenceFrequency(string inputText)
        {
            // Group characters by their value and count the occurrences of each character.
            return inputText.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        // Method to generate a priority queue based on the frequency of characters.
        // Each node in the queue represents a character and its frequency, used for building the Huffman tree.
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary)
        {
            // Initialize a priority queue with custom comparison based on frequencies (ascending order).
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));

            // Create Huffman nodes for each character and add them to the priority queue.
            foreach (var entry in frequencyDictionary)
            {
                priorityQueue.Enqueue(new HuffmanNode { Symbol = entry.Key, Frequency = entry.Value }, entry.Value);
                Console.WriteLine($"Character: {entry.Key}, Frequency: {entry.Value}"); // Debugging: Print character frequencies.
            }

            return priorityQueue;
        }
    }
}
