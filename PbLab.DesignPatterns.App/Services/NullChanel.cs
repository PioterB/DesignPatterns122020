using System;
using System.IO;

namespace PbLab.DesignPatterns.Services
{
    public class NullChanel : IChanel
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public StreamReader Connect(string resource)
        {
            return StreamReader.Null;
        }
    }
}