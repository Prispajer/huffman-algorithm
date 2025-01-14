// IHuffmanFileService.cs
namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanFileService
    {
         Task<string> ReadFileAsync(string filePath);
         Task SaveEncodedDataAsync(string filePath, string encodedData);
    }
}
