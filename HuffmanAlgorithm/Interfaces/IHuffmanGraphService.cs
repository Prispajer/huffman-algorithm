using HuffmanAlgorithm.Models;
using System.Text;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanGraphService
    {
        string GenerateDot(HuffmanNode root);
        void GenerateDotRecursive(HuffmanNode node, StringBuilder dotBuilder);
    }
}
