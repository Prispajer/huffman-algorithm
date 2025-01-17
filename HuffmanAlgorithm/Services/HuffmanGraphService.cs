using System.Text;
using HuffmanAlgorithm.Models;
using HuffmanAlgorithm.Interfaces;
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;

namespace HuffmanAlgorithm.Services
{
    // Class to handle generating a visual representation of the Huffman tree in DOT format
    public class HuffmanGraphService : IHuffmanGraphService
    {
        private int _nodeIdCounter = 0; // Counter to generate unique node IDs for .dot generation

        // Method to generate a complete DOT format string for the Huffman tree
        public string GenerateDot(HuffmanNode root)
        {
            var graph = new DotGraph().WithIdentifier("HuffmanTree").Directed(); // Create a directed graph for Huffman tree

            // Recursive method to add nodes and edges to the graph
            void AddNodeRecursive(HuffmanNode node, DotGraph dotGraph)
            {
                // Base case: if the node is null, stop recursion
                if (node == null) return;

                // Generate a unique identifier for the node
                var currentNodeIdentifier = node.Symbol != '\0'
                    ? $"{node.Symbol}"  // If the node is a leaf, use the symbol
                    : $"Node_{node.Frequency}";  // If the node is internal, use its frequency

                // Create the node and add it to the graph with its label and shape
                var currentNode = new DotNode()
                    .WithIdentifier(currentNodeIdentifier)
                    .WithLabel(node.Symbol != '\0' ? $"{node.Symbol}:{node.Frequency}" : $"{node.Frequency}")
                    .WithShape(DotNodeShape.Ellipse); // Shape the node as an ellipse

                dotGraph.Add(currentNode);  // Add the node to the graph

                // Recursively add the left child if it exists
                if (node.Left != null)
                {
                    var leftNodeIdentifier = node.Left.Symbol != '\0'
                        ? $"{node.Left.Symbol}"  // If left child is a leaf, use its symbol
                        : $"Node_{node.Left.Frequency}";  // If left child is internal, use its frequency

                    // Create an edge from the current node to the left child
                    var edge = new DotEdge()
                        .From(currentNodeIdentifier)
                        .To(leftNodeIdentifier)
                        .WithLabel("0");  // Label for the edge to represent the left branch (0)

                    dotGraph.Add(edge);  // Add the edge to the graph
                    AddNodeRecursive(node.Left, dotGraph);  // Recurse for the left child
                }

                // Recursively add the right child if it exists
                if (node.Right != null)
                {
                    var rightNodeIdentifier = node.Right.Symbol != '\0'
                        ? $"{node.Right.Symbol}"  // If right child is a leaf, use its symbol
                        : $"Node_{node.Right.Frequency}";  // If right child is internal, use its frequency

                    // Create an edge from the current node to the right child
                    var edge = new DotEdge()
                        .From(currentNodeIdentifier)
                        .To(rightNodeIdentifier)
                        .WithLabel("1");  // Label for the edge to represent the right branch (1)

                    dotGraph.Add(edge);  // Add the edge to the graph
                    AddNodeRecursive(node.Right, dotGraph);  // Recurse for the right child
                }
            }

            // Start adding nodes and edges from the root node
            AddNodeRecursive(root, graph);

            // Compile the graph into a DOT format string and return it
            using var writer = new StringWriter();  // Create a writer to write the DOT format
            var context = new CompilationContext(writer, new CompilationOptions());  // Compilation context for the graph
            graph.CompileAsync(context).GetAwaiter().GetResult();  // Compile the graph into the writer asynchronously

            return writer.ToString();  // Return the DOT format string
        }

        // Recursive method to generate the .dot representation of the Huffman tree
        public void GenerateDotRecursive(HuffmanNode node, StringBuilder dotBuilder)
        {
            // Base case: if the node is null, stop recursion
            if (node == null) return;

            // Generate a unique ID for the current node
            string currentId = $"node{_nodeIdCounter++}";  // Generate unique node ID for the DOT format

            // Add the current node to the DOT graph
            if (node.Symbol != '\0')  // If the node is a leaf
            {
                dotBuilder.AppendLine($"\"{currentId}\" [label=\"{node.Symbol}:{node.Frequency}\"];");
            }
            else  // If the node is an internal node
            {
                dotBuilder.AppendLine($"\"{currentId}\" [label=\"{node.Frequency}\", shape=ellipse];");
            }

            // Add an edge to the left child if it exists
            if (node.Left != null)
            {
                string leftId = $"node{_nodeIdCounter}";  // Generate unique ID for the left child node
                dotBuilder.AppendLine($"\"{currentId}\" -> \"{leftId}\";");  // Add edge from current node to left child
                GenerateDotRecursive(node.Left, dotBuilder);  // Recurse for the left child
            }

            // Add an edge to the right child if it exists
            if (node.Right != null)
            {
                string rightId = $"node{_nodeIdCounter}";  // Generate unique ID for the right child node
                dotBuilder.AppendLine($"\"{currentId}\" -> \"{rightId}\";");  // Add edge from current node to right child
                GenerateDotRecursive(node.Right, dotBuilder);  // Recurse for the right child
            }
        }
    }
}
