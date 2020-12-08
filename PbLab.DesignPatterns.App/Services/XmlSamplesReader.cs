﻿using System.Collections.Generic;
using System.IO;
using PbLab.DesignPatterns.Model;

namespace PbLab.DesignPatterns.Services
{
    class XmlSamplesReader : ISamplesReader
    {
        public IEnumerable<Sample> Read(StreamReader stream)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Sample> Read(IChanel chanel, string path)
        {
            throw new System.NotImplementedException();
        }
    }
}