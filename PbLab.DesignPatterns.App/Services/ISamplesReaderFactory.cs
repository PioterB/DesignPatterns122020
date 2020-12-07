using System;

namespace PbLab.DesignPatterns.Services
{
    public interface ISamplesReaderFactory
    {
        ISamplesReader Get(string key);
    }

    internal class LocalFileReaderFactory : ISamplesReaderFactory
    {
        public ISamplesReader Get(string key)
        {
            ISamplesReader result;
            switch (key)
            {
                case "json": result = new JsonSamplesReader(); break;
                case "xml": result = new XmlSamplesReader(); break;
                case "csv": result = new CsvSamplesReader(); break;
                default: throw new ArgumentOutOfRangeException("unknown reader type");
            }

            return result;
        }
    }
}