using System;
using System.IO;
using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.Services
{
    public class FileChanel : IChanel
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public StreamReader Connect(string resource)
        {
            return FileProxy.OpenText(resource);
        }
    }
}