namespace HuffmanAlgorithm.Models
{
    public class HuffmanNode
    {
        public char Symbol { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode? Left { get; set; }
        public HuffmanNode? Right { get; set; }

        public override string ToString()
        {
            string left = Left != null ? Left.ToString() : "null";
            string right = Right != null ? Right.ToString() : "null";
            return $"Symbol: {Symbol}, Frequency: {Frequency}, Left: {left}, Right: {right}";
        }

    }
}
