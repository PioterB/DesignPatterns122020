using System;
using System.Windows.Markup;

namespace PbLab.DesignPatterns.Services
{
    internal class LocalFileReaderFactory : ISamplesReaderFactory
    {
        public ISamplesReader Get(string key)
        {
            ISamplesReader result = new EmptyReader();
            switch (key)
            {
                case "json": result = new JsonSamplesReader(); break;
                case "xml": result = new XmlSamplesReader(); break;
                case "csv": result = new CsvSamplesReader(); break;
            }

            return result;
        }
    }
}