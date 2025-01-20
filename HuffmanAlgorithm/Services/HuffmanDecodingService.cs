using System.Text;
using HuffmanAlgorithm.Models;
using HuffmanAlgorithm.Interfaces;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanDecodingService : IHuffmanDecodingService
    {
        // Method to decode the encoded data using the Huffman tree
        public string DecodeHuffmanData(string encodedData, HuffmanNode root, string originalText)
        {
            // StringBuilder to accumulate the decoded text
            var decodedText = new StringBuilder();

            // Start with the root node of the Huffman tree
            var currentNode = root;

            // Keep track of the position in the original text
            int originalTextIndex = 0;

            // Loop the encoded data bit by bit
            foreach (var bit in encodedData)
            {
                // Traverse left for '0' and right for '1'
                currentNode = bit == '0' ? currentNode?.Left : currentNode?.Right;

                // If we reach a leaf node (no left or right child), it's a decoded symbol
                if (currentNode?.Left == null && currentNode?.Right == null)
                {
                    // Append the symbol of the leaf node to the decoded text
                    decodedText.Append(currentNode?.Symbol ?? '\0');

                    // Check if the corresponding character in the original text is uppercase
                    if (originalTextIndex < originalText.Length)
                    {
                        // If the character in the original text is uppercase, update the characters of decoded text to uppercase
                        if (char.IsUpper(originalText[originalTextIndex]))
                        {
                            decodedText[decodedText.Length - 1] = char.ToUpper(currentNode?.Symbol ?? '\0');
                        }

                        // Increment the index to move to the next character in the original text
                        originalTextIndex++;
                    }

                    // Reset to the root of the Huffman tree for decoding the next symbol
                    currentNode = root;
                }
            }

            // Return the fully decoded text
            return decodedText.ToString();
        }
    }
}
