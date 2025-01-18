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
            var frequencies = Enumerable.Range(0, 256)
                                        .ToDictionary(i => (byte)i, _ => 0);

            // Zlicz wystąpienia bajtów w danych wejściowych
            foreach (var b in inputData)
            {
                frequencies[b]++;
            }

            // Debugowanie częstotliwości
            Console.WriteLine("Częstotliwości bajtów:");
            foreach (var entry in frequencies)
            {
                Console.WriteLine($"Bajt: {entry.Key}, Częstotliwość: {entry.Value}");
            }

            return frequencies;
        }




        // Method to generate a priority queue based on the frequency of characters
        // The priority queue stores nodes of the Huffman tree where the key is the frequency
        // and the value is the node containing the character and its frequency
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));

            foreach (var entry in frequencyDictionary)
            {
                // Dodajemy węzeł na podstawie częstotliwości, traktując wszystko jako małe litery
                priorityQueue.Enqueue(new HuffmanNode { Symbol = entry.Key, Frequency = entry.Value }, entry.Value);
                Console.WriteLine($"TEXT: {entry.Key}, Value: {entry.Value}");
            }

            return priorityQueue;
        }


        // Method to generate a priority queue based on the frequency of bytes
        // Similar to the above method, but this works with bytes instead of characters
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<byte, int> frequencyDictionary)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => x.CompareTo(y)));

            foreach (var entry in frequencyDictionary)
            {
                if (entry.Value > 0) // Uwzględnij tylko bajty z częstotliwością > 0
                {
                    priorityQueue.Enqueue(new HuffmanNode { Symbol = (char)entry.Key, Frequency = entry.Value }, entry.Value);
                }
            }

            return priorityQueue;
        }


    }
}
