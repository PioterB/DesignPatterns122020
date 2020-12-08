using System.Collections.Generic;
using System.IO;
using System.Linq;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    public class EmptyReader : ISamplesReader
    {
        public IEnumerable<Sample> Read(StreamReader stream)
        {
            return Enumerable.Empty<Sample>();
        }

        public IEnumerable<Sample> Read(IChanel chanel, string path)
        {
            return Enumerable.Empty<Sample>();
        }
    }
}