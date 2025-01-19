using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    // This class is responsible for building the Huffman tree and generating Huffman codes for characters and bytes
    public class HuffmanTreeBuilderService : IHuffmanTreeBuilderService
    {
        // Method to generate the Huffman tree from the priority queue (works for both characters and bytes)
        public HuffmanNode GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue)
        {
            // Continue until there is only one node left in the priority queue
            while (priorityQueue.Count > 1)
            {
                // Dequeue the two nodes with the lowest frequencies
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();

                // Create a new parent node combining the two nodes
                var parent = new HuffmanNode
                {
                    Frequency = left.Frequency + right.Frequency, // Sum the frequencies of the two nodes
                    Left = left, // Assign the left child
                    Right = right // Assign the right child
                };

                // Enqueue the new parent node back into the priority queue with its combined frequency
                priorityQueue.Enqueue(parent, parent.Frequency);
            }

            // The last remaining node in the queue is the root of the Huffman tree
            var root = priorityQueue.Dequeue();
            return root;
        }

        // Recursive method to generate Huffman codes for characters
        public void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codes)
        {
            // Base case: return if the node is null
            if (node == null) return;

            // If the node represents a character (it's a leaf node), assign the current code
            if (node.Symbol != '\0') // '\0' indicates an internal node without a specific character
            {
                codes[node.Symbol] = currentCode;
            }

            // Recursively generate codes for the left child by appending '0' to the current code
            GenerateCodesRecursive(node.Left!, currentCode + "0", codes);

            // Recursively generate codes for the right child by appending '1' to the current code
            GenerateCodesRecursive(node.Right!, currentCode + "1", codes);
        }
    }
}
