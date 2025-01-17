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
            // While there is more than one element in the priority queue, combine the nodes to create the Huffman tree
            while (priorityQueue.Count > 1)
            {
                // Dequeue two nodes with the lowest frequency
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();

                // Create a new internal node with the sum of the frequencies of the two nodes
                var parent = new HuffmanNode
                {
                    Frequency = left.Frequency + right.Frequency,  // Set the frequency as the sum of the two nodes
                    Left = left,  // Assign the left child node
                    Right = right // Assign the right child node
                };

                // Enqueue the new parent node back into the priority queue
                priorityQueue.Enqueue(parent, parent.Frequency);
            }

            // Return the remaining node in the priority queue, which is the root of the Huffman tree
            return priorityQueue.Dequeue();
        }

        // Recursive method to generate Huffman codes for characters (char version)
        public void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codes)
        {
            // If the node is null, return (base case for recursion)
            if (node == null) return;

            // If the node represents a character (not an internal node), assign the current code to the character
            if (node.Symbol != '\0')
            {
                codes[node.Symbol] = currentCode; // Assign the current code to the symbol (character)
            }

            // Recursively generate code for the left child node, appending '0' to the current code
            GenerateCodesRecursive(node.Left!, currentCode + "0", codes);

            // Recursively generate code for the right child node, appending '1' to the current code
            GenerateCodesRecursive(node.Right!, currentCode + "1", codes); // Fixed here: was left
        }

        // Recursive method to generate Huffman codes for bytes (byte version)
        public void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<byte, string> codes)
        {
            // If the node is null, return (base case for recursion)
            if (node == null) return;

            // If the node represents a byte (not an internal node), assign the current code to the byte
            if (node.Symbol != '\0')
            {
                codes[(byte)node.Symbol] = currentCode; // Assign the current code to the byte (converted to byte)
            }

            // Recursively generate code for the left child node, appending '0' to the current code
            GenerateCodesRecursive(node.Left!, currentCode + "0", codes);

            // Recursively generate code for the right child node, appending '1' to the current code
            GenerateCodesRecursive(node.Right!, currentCode + "1", codes); // Fixed here: was left
        }
    }
}
