using System;
using System.IO;

namespace PbLab.DesignPatterns.Services
{
    public interface IChanel : IDisposable
    {
        StreamReader Connect(string resource);
    }
}