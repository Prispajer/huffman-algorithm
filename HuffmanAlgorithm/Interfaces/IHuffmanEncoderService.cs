using HuffmanAlgorithm.Models;
using System.Text;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanEncoderService
    {
        event Action? OnChange;
        void EncodeText();
    }
}
