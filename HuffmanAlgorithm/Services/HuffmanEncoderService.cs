using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanEncoderService : IHuffmanEncoderService
    {
        private HuffmanProcessingData _huffmanProcessingData;
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
            // Check if the input data is empty or null
            if (string.IsNullOrEmpty(_huffmanProcessingData.InputText))
                throw new ArgumentException("InputText cannot be null or empty.");

            _huffmanProcessingData.IsPending = true; // Mark the encoding process as ongoing
            _huffmanProcessingData.DecodedText = ""; // Clear previous decoded data

            // Convert the input text to lowercase, while keeping the original for later decoding
            var inputTextLower = _huffmanProcessingData.InputText.ToLower();

            // Calculate the frequency of occurrences for lowercase letters
            _huffmanProcessingData.HuffmanFrequencies = _huffmanEncodingService.CalculateOccurrenceFrequency(inputTextLower);

            // Generate the priority queue based on the occurrence frequency
            _huffmanProcessingData.HuffmanPriorityQueue = _huffmanEncodingService.GeneratePriorityQueue(_huffmanProcessingData.HuffmanFrequencies);

            // Build the Huffman tree from the priority queue
            _huffmanProcessingData.HuffmanTree = _huffmanTreeBuilderService.GenerateHuffmanTree(_huffmanProcessingData.HuffmanPriorityQueue);

            _huffmanProcessingData.HuffmanCodes = new Dictionary<char, string>(); // Initialize the dictionary for Huffman codes

            // Recursively generate Huffman codes for each symbol in the tree
            _huffmanTreeBuilderService.GenerateCodesRecursive(_huffmanProcessingData.HuffmanTree, "", _huffmanProcessingData.HuffmanCodes);

            // Generate the encoded text using Huffman codes based on lowercase letters
            _huffmanProcessingData.EncodedText = string.Join("", inputTextLower.Select(c =>
            {
                // If a Huffman code exists for this symbol, return it, otherwise return an empty string
                return _huffmanProcessingData.HuffmanCodes.ContainsKey(c) ? _huffmanProcessingData.HuffmanCodes[c] : "";
            }));

            _huffmanProcessingData.IsPending = false; // Mark the encoding process as finished
            OnChange?.Invoke(); // Trigger the event to notify that encoding is completed
        }

        // Method to encode binary data using Huffman encoding
        public void EncodeBinaryData(byte[] inputData)
        {
            Console.WriteLine("Starting binary data encoding...");
            Console.WriteLine($"Input data size: {inputData.Length} bytes");

            // Calculate frequency occurrences for binary data
            _huffmanProcessingData.HuffmanBinaryFrequencies = _huffmanEncodingService.CalculateOccurrenceFrequency(inputData);
            Console.WriteLine("Binary frequencies calculated.");

            // Generate the priority queue for binary data
            _huffmanProcessingData.HuffmanPriorityQueue = _huffmanEncodingService.GeneratePriorityQueue(_huffmanProcessingData.HuffmanBinaryFrequencies);
            Console.WriteLine("Binary priority queue generated.");

            // Generate the Huffman tree for binary data
            _huffmanProcessingData.HuffmanTree = _huffmanTreeBuilderService.GenerateHuffmanTree(_huffmanProcessingData.HuffmanPriorityQueue);
            Console.WriteLine("Binary Huffman tree generated.");

            _huffmanProcessingData.HuffmanBinaryCodes = new Dictionary<byte, string>(); // Initialize dictionary for binary Huffman codes
            _huffmanTreeBuilderService.GenerateCodesRecursive(_huffmanProcessingData.HuffmanTree, "", _huffmanProcessingData.HuffmanBinaryCodes);
            Console.WriteLine("Binary Huffman codes generated.");

            Console.WriteLine("Encoding binary data...");
            var encodedBits = new List<byte>();

            // Convert input data to Huffman encoded bits
            foreach (var b in inputData)
            {
                if (_huffmanProcessingData.HuffmanBinaryCodes.TryGetValue(b, out var code))
                {
                    encodedBits.AddRange(code.Select(c => c == '1' ? (byte)1 : (byte)0));
                }
            }

            // Print out the generated Huffman codes for binary data
            Console.WriteLine("Generated Huffman codes:");
            foreach (var kvp in _huffmanProcessingData.HuffmanBinaryCodes)
            {
                Console.WriteLine($"Byte: {kvp.Key}, Code: {kvp.Value}");
            }

            // Store the encoded binary data
            _huffmanProcessingData.EncodedBinaryData = encodedBits.ToArray();
            Console.WriteLine("Binary data encoding completed.");
            Console.WriteLine($"Encoded binary data length: {_huffmanProcessingData.EncodedBinaryData.Length}");
        }
    }
}
