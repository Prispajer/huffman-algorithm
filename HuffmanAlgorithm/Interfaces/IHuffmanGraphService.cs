using HuffmanAlgorithm.Models;
using DotNetGraph.Core;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanGraphService
    {
        string GenerateDot(HuffmanNode root);
        void AddNodeRecursive(HuffmanNode node, DotGraph dotGraph);
    }
}
