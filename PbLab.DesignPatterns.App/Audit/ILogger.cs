using System;

namespace PbLab.DesignPatterns.Audit
{
    public interface ILogger
    {
        void Log(string message);

        event Action<string> NewEntry;
    }
}