// IHuffmanFileService.cs
using Microsoft.AspNetCore.Components.Forms;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanFileService
    {
        Task<byte[]> ReadFileAsBytesAsync(IBrowserFile file);
        Task<string> ReadFileAsTextAsync(IBrowserFile file);
    }
}
