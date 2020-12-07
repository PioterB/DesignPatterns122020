using System;
using System.Collections.Generic;
using System.IO;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    public class CsvSamplesReader: ISamplesReader
    {
        public IEnumerable<Sample> Read(StreamReader stream)
        {
            var results = new List<Sample>();
            var line = stream.ReadLine();
            while (string.IsNullOrEmpty(line) == false)
            {
                var parts = line.Split(';');
                
                var massParts = parts[1].Split((' '));
                var value = decimal.Parse(massParts[0]);
                var unit = (MassUnit)Enum.Parse(typeof(MassUnit), massParts[1]);
                var result = new MassValue(value, unit);

                var sample = new Sample(DateTimeOffset.Parse(parts[0]), result);
                
                results.Add(sample);
                line = stream.ReadLine();
            }

            return results;
        }
    }
}