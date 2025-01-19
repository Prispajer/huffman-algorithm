namespace HuffmanAlgorithm.Models
{
    public class HuffmanNode
    {
        // The symbol (character) associated with the node
        public char Symbol { get; set; }

        // The frequency of the symbol in the data
        public int Frequency { get; set; }

        // Left child node (for building the Huffman tree)
        public HuffmanNode? Left { get; set; }

        // Right child node (for building the Huffman tree)
        public HuffmanNode? Right { get; set; }

        // Override the ToString method to display the node's details
        public override string ToString()
        {
            // If the left node is not null, display its details; otherwise, show "null"
            string left = Left != null ? Left.ToString() : "null";

            // If the right node is not null, display its details; otherwise, show "null"
            string right = Right != null ? Right.ToString() : "null";

            // Return a formatted string with the symbol, frequency, and children nodes
            return $"Symbol: {Symbol}, Frequency: {Frequency}, Left: {left}, Right: {right}";
        }
    }
}
