using System;

namespace PbLab.DesignPatterns.Audit
{
    internal class TimeStampDecorator : ILogger
    {
        private readonly ILogger _inner;

        public TimeStampDecorator(ILogger inner)
        {
            _inner = inner;
        }

        public void Log(string message)
        {
            var thread = CurrentTime();
            _inner.Log($"[{thread}]  {message}");
        }

        private string CurrentTime()
        {
            return DateTime.Now.ToString();
        }

        public event Action<string> NewEntry;
    }
}