using System.Collections.Generic;
using System.IO;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    public interface ISamplesReader
    {
        IEnumerable<Sample> Read(StreamReader stream);
    }
}