using System.Text;
using HuffmanAlgorithm.Models;
using HuffmanAlgorithm.Interfaces;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanDecodingService : IHuffmanDecodingService
    {
        // Method to decode the encoded data using the Huffman tree
        public string DecodeHuffmanData(string encodedData, HuffmanNode root)
        {
            var decodedText = new StringBuilder(); // StringBuilder to accumulate the decoded text
            var currentNode = root; // Start with the root node of the Huffman tree

            // Traverse the encoded data bit by bit
            foreach (var bit in encodedData)
            {
                // Traverse left for '0' and right for '1'
                currentNode = bit == '0' ? currentNode?.Left : currentNode?.Right;

                // If we reach a leaf node (no left or right child), it's a decoded symbol
                if (currentNode?.Left == null && currentNode?.Right == null)
                {
                    decodedText.Append(currentNode?.Symbol); // Append the symbol from the leaf node
                    currentNode = root; // Reset to the root for the next character
                }
            }

            return decodedText.ToString(); // Return the decoded string
        }
    }
}
