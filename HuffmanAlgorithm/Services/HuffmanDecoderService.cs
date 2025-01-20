using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanDecoderService : IHuffmanDecoderService
    {
        private HuffmanProcessingData _huffmanProcessingData;
        private readonly IHuffmanTreeBuilderService _huffmanTreeBuilderService;
        private readonly IHuffmanEncodingService _huffmanEncodingService; 
        private readonly IHuffmanDecodingService _huffmanDecodingService; 

        public event Action? OnChange; // Event triggered when the state changes (for updating UI or other listeners)

        // Constructor to initialize the service with HuffmanProcessingData and other dependencies
        public HuffmanDecoderService(HuffmanProcessingData huffmanProcessingData,
                                      IHuffmanTreeBuilderService huffmanTreeBuilderService,
                                      IHuffmanEncodingService huffmanEncodingService,
                                      IHuffmanDecodingService huffmanDecodingService)
        {
            _huffmanProcessingData = huffmanProcessingData; 
            _huffmanTreeBuilderService = huffmanTreeBuilderService; 
            _huffmanEncodingService = huffmanEncodingService; 
            _huffmanDecodingService = huffmanDecodingService; 
        }

        // Method to decode the encoded text back to the original text
        public void DecodeText()
        {
            // Ensure the encoded text and original input text are not null or empty
            if (string.IsNullOrEmpty(_huffmanProcessingData.EncodedText) || string.IsNullOrEmpty(_huffmanProcessingData.InputText))
                return;

            // Decode the encoded text using the Huffman tree
            _huffmanProcessingData.DecodedText = _huffmanDecodingService.DecodeHuffmanData(_huffmanProcessingData.EncodedText, _huffmanProcessingData.HuffmanTree ?? new HuffmanNode(), _huffmanProcessingData.InputText);

            // Notify listeners that the state has changed (this could trigger UI updates, etc.)
            OnChange?.Invoke();
        }
    }
}
