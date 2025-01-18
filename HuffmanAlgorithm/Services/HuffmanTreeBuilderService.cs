using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    // This class is responsible for building the Huffman tree and generating Huffman codes for characters and bytes
    public class HuffmanTreeBuilderService : IHuffmanTreeBuilderService
    {
        // Method to generate the Huffman tree from the priority queue (works for both char and byte)
        public HuffmanNode GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue)
        {
            Console.WriteLine("Starting to build the Huffman tree...");

            // Check if the queue has more than one element
            while (priorityQueue.Count > 1)
            {
                // Dequeue two smallest elements (lowest frequencies)
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();

                Console.WriteLine($"Dequeue left node with frequency: {left.Frequency}, right node with frequency: {right.Frequency}");

                // Create a new parent node that combines the two smallest elements
                var parent = new HuffmanNode
                {
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };

                // Enqueue the new parent node back into the priority queue
                priorityQueue.Enqueue(parent, parent.Frequency);

                Console.WriteLine($"Enqueue parent node with combined frequency: {parent.Frequency}");
            }

            // Return the last remaining element, which is the root of the Huffman tree
            var root = priorityQueue.Dequeue();
            Console.WriteLine("Huffman tree built successfully!");
            return root;
        }

        // Recursive method to generate Huffman codes for characters (char version)
        public void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codes)
        {
            if (node == null) return;

            // If the node represents a character (not an internal node), assign the current code to the character
            if (node.Symbol != '\0')
            {
                codes[node.Symbol] = currentCode;
                Console.WriteLine($"Generated code for character '{node.Symbol}': {currentCode}");
            }

            // Recursively generate code for the left child node, appending '0' to the current code
            GenerateCodesRecursive(node.Left!, currentCode + "0", codes);

            // Recursively generate code for the right child node, appending '1' to the current code
            GenerateCodesRecursive(node.Right!, currentCode + "1", codes);
        }

        // Recursive method to generate Huffman codes for bytes (byte version)
        public void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<byte, string> codes)
        {
            if (node == null) return;

            if (node.Symbol != '\0') // Upewnij się, że symbol jest poprawnie przypisany
            {
                codes[(byte)node.Symbol] = currentCode;
                Console.WriteLine($"Generated code for byte '{(byte)node.Symbol}': {currentCode}");
            }

            GenerateCodesRecursive(node.Left, currentCode + "0", codes);
            GenerateCodesRecursive(node.Right, currentCode + "1", codes);
        }

    }
}
