using HuffmanAlgorithm.Models;

namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanEncoderService
    {
        // Metoda generująca kody Huffmana na podstawie podanego tekstu
        Dictionary<char, string> GenerateHuffmanCodes(string inputText);

        // Zlicza częstotliwość wystąpienia znaku na podstawie klucza kolekcji danych
        Dictionary<char, int> CalculateOccurrenceFrequency(string inputText);

        // Tworzy kolejkę priorytetową na podstawie częstotliwości występowania znaków
        PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary);

        // Metoda generująca drzewo Huffmana
        void GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue);

        // Rekurencyjnie generuje binarne kody Huffmana dla każdego symbolu
        void GenerateCodesRecursive(HuffmanNode node, string code, Dictionary<char, string> codes);
    }
}
