using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanEncoderService : IHuffmanEncoderService
    {
        private readonly HuffmanProcessingData _huffmanProcessingData;
        private readonly IHuffmanTreeBuilderService _huffmanTreeBuilderService;
        private readonly IHuffmanEncodingService _huffmanEncodingService;
        public event Action? OnChange;

        // Constructor to initialize the services required for Huffman encoding
        public HuffmanEncoderService(
            HuffmanProcessingData huffmanProcessingData,
            IHuffmanTreeBuilderService huffmanTreeBuilderService,
            IHuffmanEncodingService huffmanEncodingService)
        {
            _huffmanProcessingData = huffmanProcessingData;
            _huffmanEncodingService = huffmanEncodingService;
            _huffmanTreeBuilderService = huffmanTreeBuilderService;
        }

        // Method to encode the input text using Huffman encoding
        public void EncodeText()
        {
            // Check if the input text is null or empty
            if (string.IsNullOrEmpty(_huffmanProcessingData.InputText))
                throw new ArgumentException("InputText cannot be null or empty.");

            _huffmanProcessingData.IsPending = true; // Mark the encoding process as pending
            _huffmanProcessingData.DecodedText = ""; // Clear the decoded text (if any)

            // Generate frequencies of character occurrences in the input text
            _huffmanProcessingData.HuffmanFrequencies = _huffmanEncodingService.CalculateOccurrenceFrequency(_huffmanProcessingData.InputText);

            // Generate a priority queue based on character frequencies
            _huffmanProcessingData.HuffmanPriorityQueue = _huffmanEncodingService.GeneratePriorityQueue(_huffmanProcessingData.HuffmanFrequencies);

            // Generate the Huffman tree based on the priority queue
            _huffmanProcessingData.HuffmanTree = _huffmanTreeBuilderService.GenerateHuffmanTree(_huffmanProcessingData.HuffmanPriorityQueue);

            _huffmanProcessingData.HuffmanCodes = new Dictionary<char, string>(); // Initialize the dictionary to store Huffman codes

            // Recursively generate Huffman codes for each character in the tree
            _huffmanTreeBuilderService.GenerateCodesRecursive(_huffmanProcessingData.HuffmanTree, "", _huffmanProcessingData.HuffmanCodes);

            // Generate the encoded text by joining Huffman codes for each character in the input text
            _huffmanProcessingData.EncodedText = string.Join("", _huffmanProcessingData.InputText.Select(c =>
            {
                // If the Huffman code exists for the character, return it, otherwise return an empty string
                if (_huffmanProcessingData.HuffmanCodes.ContainsKey(c))
                {
                    return _huffmanProcessingData.HuffmanCodes[c];
                }
                else
                {
                    return ""; // Return a default value (e.g., an empty string) if no code is found for the character
                }
            }));

            _huffmanProcessingData.IsPending = false; // Mark the encoding process as complete
            OnChange?.Invoke(); // Trigger an event to notify listeners that the encoding is done
        }

        // Method to encode binary data using Huffman encoding
        public void EncodeBinaryData(byte[] inputData)
        {
            // Check if the input binary data is null or empty
            if (inputData == null || inputData.Length == 0)
            {
                throw new ArgumentException("Input binary data cannot be null or empty.");
            }

            _huffmanProcessingData.IsPending = true; // Mark the encoding process as pending
            _huffmanProcessingData.EncodedBinaryData = null; // Clear any previously encoded binary data

            // Calculate the frequency of byte occurrences in the input binary data
            _huffmanProcessingData.HuffmanBinaryFrequencies = _huffmanEncodingService.CalculateOccurrenceFrequency(inputData);

            // Generate a priority queue based on byte frequencies
            _huffmanProcessingData.HuffmanPriorityQueue = _huffmanEncodingService.GeneratePriorityQueue(_huffmanProcessingData.HuffmanBinaryFrequencies);

            // Generate the Huffman tree for binary data
            _huffmanProcessingData.HuffmanTree = _huffmanTreeBuilderService.GenerateHuffmanTree(_huffmanProcessingData.HuffmanPriorityQueue);

            // Generate Huffman codes for each byte
            _huffmanProcessingData.HuffmanBinaryCodes = new Dictionary<byte, string>();
            _huffmanTreeBuilderService.GenerateCodesRecursive(_huffmanProcessingData.HuffmanTree, "", _huffmanProcessingData.HuffmanBinaryCodes);

            // Encode the input binary data by converting each byte into its corresponding Huffman code
            var encodedBits = new List<byte>();
            foreach (var b in inputData)
            {
                if (_huffmanProcessingData.HuffmanBinaryCodes.TryGetValue(b, out var code))
                {
                    // Convert each bit in the Huffman code to a byte (1 or 0)
                    encodedBits.AddRange(code.Select(c => c == '1' ? (byte)1 : (byte)0));
                }
                else
                {
                    throw new InvalidOperationException($"Byte {b} not found in Huffman codes.");
                }
            }

            // Store the encoded binary data as a byte array
            _huffmanProcessingData.EncodedBinaryData = encodedBits.ToArray();
            _huffmanProcessingData.IsPending = false; // Mark the encoding process as complete
            OnChange?.Invoke(); // Trigger an event to notify listeners that the encoding is done
        }
    }
}
