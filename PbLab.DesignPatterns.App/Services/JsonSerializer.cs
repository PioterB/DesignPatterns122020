using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    public class JsonSamplesReader : ISamplesReader
    {
        public IEnumerable<Sample> Read(StreamReader stream)
        {
            JsonSerializer serializer = new JsonSerializer();
            using (JsonReader r = new JsonTextReader(stream))
            {
                return serializer.Deserialize<List<Sample>>(r);
            }
        }
    }
}