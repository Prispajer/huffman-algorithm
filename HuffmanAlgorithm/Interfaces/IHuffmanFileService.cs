// IHuffmanFileService.cs
namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanFileService
    {
        Task<byte[]> ReadBinaryFileAsync(string filePath);
        Task SaveEncodedBinaryDataAsync(string filePath, byte[] encodedData);
    }
}
