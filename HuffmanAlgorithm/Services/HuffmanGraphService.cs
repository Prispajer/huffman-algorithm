using HuffmanAlgorithm.Interfaces;
using HuffmanAlgorithm.Models;
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;

namespace HuffmanAlgorithm.Services
{
    // Class to handle generating a visual representation of the Huffman tree in DOT format
    public class HuffmanGraphService : IHuffmanGraphService
    {
        private HuffmanProcessingData _huffmanProcessingData;

        public HuffmanGraphService(HuffmanProcessingData huffmanProcessingData)
        {
            _huffmanProcessingData = huffmanProcessingData;
        }

        // Method to generate a complete DOT format string for the Huffman tree
        public string GenerateDot(HuffmanNode root)
        {
            var graph = new DotGraph().WithIdentifier("HuffmanTree").Directed(); // Create a directed graph for Huffman tree

            // Use the NodeIdCounter from the HuffmanProcessingData model
            AddNodeRecursive(root, graph);  // Start adding from the root node

            // Compile the graph into a DOT format string and return it
            using var writer = new StringWriter();  // Create a writer to write the DOT format
            var context = new CompilationContext(writer, new CompilationOptions());  // Compilation context for the graph
            graph.CompileAsync(context).GetAwaiter().GetResult();  // Compile the graph into the writer asynchronously

            return writer.ToString();  // Return the DOT format string
        }

        // Recursive method to add nodes and edges to the graph
        public void AddNodeRecursive(HuffmanNode node, DotGraph dotGraph)
        {
            if (node == null) return;

            // Generate a unique node identifier (using counter to ensure uniqueness)
            var currentNodeIdentifier = $"Node_{_huffmanProcessingData.NodeIdCounter++}";  // Use the counter

            // Create the node with label and shape
            var currentNode = new DotNode()
                .WithIdentifier(currentNodeIdentifier)
                .WithLabel(node.Symbol != '\0' ? $"{node.Symbol}:{node.Frequency}" : $"{node.Frequency}")
                .WithShape(DotNodeShape.Ellipse);

            dotGraph.Add(currentNode);  // Add the node to the graph

            // Recursively add edges and nodes for the left and right children
            if (node.Left != null)
            {
                var leftNodeIdentifier = $"Node_{_huffmanProcessingData.NodeIdCounter}";  // Unique identifier for left child
                var edge = new DotEdge()
                    .From(currentNodeIdentifier)
                    .To(leftNodeIdentifier)
                    .WithLabel("0");
                dotGraph.Add(edge);  // Add the left edge to the graph
                AddNodeRecursive(node.Left, dotGraph);  // Recurse for the left child
            }

            if (node.Right != null)
            {
                var rightNodeIdentifier = $"Node_{_huffmanProcessingData.NodeIdCounter}";  // Unique identifier for right child
                var edge = new DotEdge()
                    .From(currentNodeIdentifier)
                    .To(rightNodeIdentifier)
                    .WithLabel("1");
                dotGraph.Add(edge);  // Add the right edge to the graph
                AddNodeRecursive(node.Right, dotGraph);  // Recurse for the right child
            }
        }
    }

}
