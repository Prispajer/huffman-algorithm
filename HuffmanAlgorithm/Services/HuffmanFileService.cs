using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanFileService : IHuffmanFileService
    {
        // Method to read a file as binary data (byte array)
        public async Task<byte[]> ReadFileAsBytesAsync(IBrowserFile file)
        {
            try
            {
                // Open the file stream
                using var stream = file.OpenReadStream();
                // Create a memory stream to hold the file's byte data
                using var memoryStream = new MemoryStream();
                // Copy the file data to the memory stream
                await stream.CopyToAsync(memoryStream);
                // Return the byte array of the file
                return memoryStream.ToArray();
            }
            catch (Exception ex)
            {
                // If an error occurs, return an empty byte array
                return Array.Empty<byte>();
            }
        }

        // Method to read a file as string text
        public async Task<string> ReadFileAsTextAsync(IBrowserFile file)
        {
            try
            {
                // Open the file stream
                using var stream = file.OpenReadStream();
                // Create a stream reader to read the file text
                using var reader = new StreamReader(stream);
                // Read the entire file content as a string
                return await reader.ReadToEndAsync();
            }
            catch (Exception ex)
            {
                // If an error occurs, return an error message
                return $"Error reading text file: {ex.Message}";
            }
        }
    }
}
