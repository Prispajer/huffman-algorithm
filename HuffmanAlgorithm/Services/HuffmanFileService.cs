// HuffmanFileService.cs
using HuffmanAlgorithm.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanFileService: IHuffmanFileService
    {
        // Odczytanie pliku jako tekst
        public async Task<string> ReadFileAsync(string filePath)
        {
            return await File.ReadAllTextAsync(filePath);
        }

        // Zapisanie zakodowanych danych do pliku
        public async Task SaveEncodedDataAsync(string filePath, string encodedData)
        {
             await File.WriteAllTextAsync(filePath, encodedData);
        }
    }
}