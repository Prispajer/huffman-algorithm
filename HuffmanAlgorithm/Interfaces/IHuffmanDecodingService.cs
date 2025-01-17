using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanDecodingService
    {
        string DecodeHuffmanData(string encodedData, HuffmanNode root);
    }
}
