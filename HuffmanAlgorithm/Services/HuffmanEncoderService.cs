using System.Text;
using HuffmanAlgorithm.Models;
using HuffmanAlgorithm.Interfaces;
using DotNetGraph.Compilation;
using DotNetGraph.Core;
using DotNetGraph.Extensions;


namespace HuffmanAlgorithm.Services
{
    public class HuffmanEncoderService : IHuffmanEncoderService
    {

        private int nodeIdCounter = 0;

        // Główna metoda generująca kody Huffmana z tekstu wejściowego
        public Dictionary<char, string> GenerateHuffmanCodes(string inputText)
        {
            var frequencyDictionary = CalculateOccurrenceFrequency(inputText);
            var priorityQueue = GeneratePriorityQueue(frequencyDictionary);
            var root = GenerateHuffmanTree(priorityQueue);

            var codes = new Dictionary<char, string>();
            GenerateCodesRecursive(root, "", codes);
            return codes;
        }

        // Zlicza częstotliwość wystąpienia znaków
        public Dictionary<char, int> CalculateOccurrenceFrequency(string inputText)
        {
            return inputText.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        // Tworzy kolejkę priorytetową na podstawie częstotliwości znaków
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => x.CompareTo(y))); // Min-heap
            foreach (var entry in frequencyDictionary)
            {
                priorityQueue.Enqueue(new HuffmanNode { Symbol = entry.Key, Frequency = entry.Value }, entry.Value);
            }
            return priorityQueue;
        }

        // Generuje drzewo Huffmana
        public HuffmanNode GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue)
        {
            while (priorityQueue.Count > 1)
            {
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();
                var parent = new HuffmanNode
                {
                    Frequency = left.Frequency + right.Frequency,
                    Left = left,
                    Right = right
                };
                priorityQueue.Enqueue(parent, parent.Frequency);
            }

            return priorityQueue.Dequeue();
        }

        // Rekurencyjnie generuje kody binarne Huffmana
        public void GenerateCodesRecursive(HuffmanNode node, string currentCode, Dictionary<char, string> codes)
        {
            if (node == null) return;

            if (node.Symbol != '\0')
            {
                codes[node.Symbol] = currentCode;
            }

            GenerateCodesRecursive(node.Left!, currentCode + "0", codes);
            GenerateCodesRecursive(node.Right!, currentCode + "1", codes);
        }

        // Generuje .dot dla drzewa Huffmana
        public string GenerateDot(HuffmanNode root)
        {
            var graph = new DotGraph().WithIdentifier("HuffmanTree").Directed();

            void AddNodeRecursive(HuffmanNode node, DotGraph dotGraph)
            {
                if (node == null) return;

                // Generowanie unikalnego identyfikatora dla węzła
                var currentNodeIdentifier = node.Symbol != '\0'
                    ? $"{node.Symbol}" // Jeśli symbol istnieje
                    : $"Node_{node.Frequency}"; // Jeśli to węzeł wewnętrzny

                var currentNode = new DotNode()
                    .WithIdentifier(currentNodeIdentifier)
                    .WithLabel(node.Symbol != '\0' ? $"{node.Symbol}:{node.Frequency}" : $"{node.Frequency}")
                    .WithShape(DotNodeShape.Ellipse);

                dotGraph.Add(currentNode);

                if (node.Left != null)
                {
                    var leftNodeIdentifier = node.Left.Symbol != '\0'
                        ? $"{node.Left.Symbol}" // Symbol liścia
                        : $"Node_{node.Left.Frequency}"; // Węzeł wewnętrzny

                    var edge = new DotEdge()
                        .From(currentNodeIdentifier)
                        .To(leftNodeIdentifier)
                        .WithLabel("0"); // Lewa krawędź (0)

                    dotGraph.Add(edge);
                    AddNodeRecursive(node.Left, dotGraph);
                }

                if (node.Right != null)
                {
                    var rightNodeIdentifier = node.Right.Symbol != '\0'
                        ? $"{node.Right.Symbol}" // Symbol liścia
                        : $"Node_{node.Right.Frequency}"; // Węzeł wewnętrzny

                    var edge = new DotEdge()
                        .From(currentNodeIdentifier)
                        .To(rightNodeIdentifier)
                        .WithLabel("1"); // Prawa krawędź (1)

                    dotGraph.Add(edge);
                    AddNodeRecursive(node.Right, dotGraph);
                }
            }


            AddNodeRecursive(root, graph);

            // Kompilacja do DOT
            using var writer = new StringWriter();
            var context = new CompilationContext(writer, new CompilationOptions());
            graph.CompileAsync(context).GetAwaiter().GetResult();

            return writer.ToString();
        }

        // Rekurencyjne generowanie .dot
        public void GenerateDotRecursive(HuffmanNode node, StringBuilder dotBuilder)
        {
            if (node == null) return;

            string currentId = $"node{nodeIdCounter++}"; // Unikalny identyfikator węzła

            // Dodajemy węzeł do grafu
            if (node.Symbol != '\0')
            {
                dotBuilder.AppendLine($"\"{currentId}\" [label=\"{node.Symbol}:{node.Frequency}\"];");
            }
            else
            {
                dotBuilder.AppendLine($"\"{currentId}\" [label=\"{node.Frequency}\", shape=ellipse];");
            }

            if (node.Left != null)
            {
                string leftId = $"node{nodeIdCounter}";
                dotBuilder.AppendLine($"\"{currentId}\" -> \"{leftId}\";");
                GenerateDotRecursive(node.Left, dotBuilder);
            }

            if (node.Right != null)
            {
                string rightId = $"node{nodeIdCounter}";
                dotBuilder.AppendLine($"\"{currentId}\" -> \"{rightId}\";");
                GenerateDotRecursive(node.Right, dotBuilder);
            }
        }

        // Dekodowanie zakodowanego tekstu
        public string DecodeHuffmanData(string encodedData, HuffmanNode root)
        {
            var decodedText = new StringBuilder();
            var currentNode = root;

            foreach (var bit in encodedData)
            {
                currentNode = bit == '0' ? currentNode?.Left : currentNode?.Right;

                if (currentNode?.Left == null && currentNode?.Right == null)
                {
                    decodedText.Append(currentNode?.Symbol);
                    currentNode = root; // Resetujemy do korzenia
                }
            }

            return decodedText.ToString();
        }



    }
}
