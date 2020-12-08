using PbLab.DesignPatterns.Audit;

namespace PbLab.DesignPatterns.Services
{
    public interface ISamplesReaderFactory
    {
        ISamplesReader Get(string key);
    }
}