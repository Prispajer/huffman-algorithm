namespace HuffmanAlgorithm.Models
{
    public class HuffmanProcessingData
    {
        public string InputText { get; set; } = ""; // Input text for encoding/decoding
        public string? EncodedText { get; set; } = ""; // The result of the encoded text
        public string? DecodedText { get; set; } = ""; // The result of the decoded text
        public bool IsPending { get; set; } = false; // Flag indicating if the process is ongoing
        public int NodeIdCounter { get; set; } = 0; // Counter for generating unique IDs
        public Dictionary<char, int>? HuffmanFrequencies { get; set; } // Frequencies for text data
        public PriorityQueue<HuffmanNode, int>? HuffmanPriorityQueue { get; set; } // Priority queue to store Huffman occurrence frequency
        public HuffmanNode? HuffmanTree { get; set; } // Huffman tree
        public Dictionary<char, string>? HuffmanCodes { get; set; } // Huffman codes for characters
    }
}
