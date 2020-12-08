using System;

namespace PbLab.DesignPatterns.Audit
{
    internal class ComposedLogger : ILogger
    {
        public void Log(string message)
        {
            Write(message);
        }

        private void Write(string message)
        {
            NewEntry?.Invoke(Compose(message));
        }

        public event Action<string> NewEntry;

        private string Compose(string message)
        {
            return message;
        }
    }
}