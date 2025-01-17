using HuffmanAlgorithm.Interfaces;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanFileService : IHuffmanFileService
    {
        // Method to read a file as binary data (byte array)
        public async Task<byte[]> ReadBinaryFileAsync(string filePath)
        {
            // Read the entire file and return it as a byte array
            return await File.ReadAllBytesAsync(filePath);
        }

        // Method to save the encoded binary data to a file
        public async Task SaveEncodedBinaryDataAsync(string filePath, byte[] encodedData)
        {
            // Write the encoded binary data to the specified file
            await File.WriteAllBytesAsync(filePath, encodedData);
        }
    }
}
