using System;
using System.Collections.Generic;
using System.Linq;
using HuffmanAlgorithm.Models;
using HuffmanAlgorithm.Interfaces;
using Microsoft.AspNetCore.Components.Forms;

namespace HuffmanAlgorithm.Services
{
    public class HuffmanEncoderService : IHuffmanEncoderService
    {
        // Główna metoda generująca kody Huffmana na podstawie podanego tekstu
        public Dictionary<char, string> GenerateHuffmanCodes(string inputText)
        {
            // 1. Zliczanie częstotliwości wystąpień każdego znaku w tekście
            var frequencyDictionary = CalculateOccurrenceFrequency(inputText);

            // 2. Utworzenie kolejki priorytetowej na podstawie częstotliwości znaków
            var priorityQueue = GeneratePriorityQueue(frequencyDictionary);

            // 3. Wygenerowanie drzewa Huffmana na podstawie kolejki priorytetowej
            GenerateHuffmanTree(priorityQueue);

            // 4. Generowanie kodów binarnych dla każdego symbolu na podstawie drzewa Huffmana
            var codes = new Dictionary<char, string>();
            GenerateCodesRecursive(priorityQueue.Dequeue(), "", codes);

            // 5. Zwracanie mapy kodów Huffmana (znak -> kod binarny)
            return codes;
        }


        // Zlicza częstotliwość wystąpienia znaku na podstawie klucza kolekcji danych
        public Dictionary<char, int> CalculateOccurrenceFrequency(string inputText)
        {
            return inputText.GroupBy(c => c).ToDictionary(g => g.Key, g => g.Count());
        }

        // Tworzy kolejkę priorytetową na podstawie częstotliwości występowania znaków
        public PriorityQueue<HuffmanNode, int> GeneratePriorityQueue(Dictionary<char, int> frequencyDictionary)
        {
            var priorityQueue = new PriorityQueue<HuffmanNode, int>(Comparer<int>.Create((x, y) => y.CompareTo(x)));

            // Dodanie każdego znaku do kolejki jako osobnego węzła
            foreach (var entry in frequencyDictionary)
            {
                priorityQueue.Enqueue(new HuffmanNode
                {
                    Symbol = entry.Key,      // Znak
                    Frequency = entry.Value  // Częstotliwość wystąpienia
                }, entry.Value); // Używamy częstotliwości jako priorytetu
            }

            return priorityQueue;
        }


        // Generuje drzewo Huffmana na podstawie kolejki priorytetowej
        public void GenerateHuffmanTree(PriorityQueue<HuffmanNode, int> priorityQueue)
        {
            // Dopóki w kolejce jest więcej niż jeden węzeł
            while (priorityQueue.Count > 1)
            {
                // Pobierz dwa węzły o najniższym priorytecie
                var left = priorityQueue.Dequeue();
                var right = priorityQueue.Dequeue();

                // Utwórz nowy węzeł jako rodzic dla dwóch pobranych
                var parentNode = new HuffmanNode
                {
                    Symbol = '\0', // Węzeł wewnętrzny nie przechowuje symbolu
                    Frequency = right.Frequency + left.Frequency,
                    Left = left,
                    Right = right
                };

                // Logowanie tylko liści (symboli)
                if (left.Symbol != '\0')
                    Console.WriteLine($"Left: {left.Symbol}, Frequency: {left.Frequency}");
                if (right.Symbol != '\0')
                    Console.WriteLine($"Right: {right.Symbol}, Frequency: {right.Frequency}");

                // Dodaj nowy węzeł z powrotem do kolejki
                priorityQueue.Enqueue(parentNode, parentNode.Frequency);
            }
        }



        // Rekurencyjnie generuje binarne kody Huffmana dla każdego symbolu
        public void GenerateCodesRecursive(HuffmanNode node, string code, Dictionary<char, string> codes)
        {
            if (node == null) return;

            if (node.Symbol != '\0') // Jeśli to liść, zapisujemy symbol
            {
                codes[node.Symbol] = code;
                Console.WriteLine($"Symbol: {node.Symbol}, Code: {code}");
            }

            // Rekurencyjnie przechodzimy po lewym dziecku (dodajemy '0') i prawym dziecku (dodajemy '1')
            if (node.Left != null)
                GenerateCodesRecursive(node.Left, code + "0", codes);

            if (node.Right != null)
                GenerateCodesRecursive(node.Right, code + "1", codes);
        }



    }
}
