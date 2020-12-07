using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    [TestClass]
    public class CsvSamplesReaderTests
    {
        [TestMethod]
        public void Read_StreamWithSamples_Read()
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.WriteLine("2020-12-01 20:32;10 Kilogram");
            writer.Flush();
            stream.Position = 0;
            
            var reader = new CsvSamplesReader();

            var samples = reader.Read(new StreamReader(stream));

            Assert.AreEqual(1, samples.Count());
            Assert.AreEqual(MassUnit.Kilogram, samples.First().Mass.Unit);
        }
    }
}
