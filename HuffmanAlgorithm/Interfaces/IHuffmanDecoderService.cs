namespace HuffmanAlgorithm.Interfaces
{
    public interface IHuffmanDecoderService
    {
        event Action? OnChange;
        void DecodeText();
    }
}
