using System;
using System.Threading;

namespace PbLab.DesignPatterns.Audit
{
    public class ThreadDecorator : ILogger
    {
        private readonly ILogger _inner;

        public ThreadDecorator(ILogger inner)
        {
            _inner = inner;
        }

        public void Log(string message)
        {
            var thread = ReadThreadIdentity();
            _inner.Log($"[{thread}]  {message}");
        }

        private string ReadThreadIdentity()
        {
            return Thread.CurrentThread.ManagedThreadId.ToString();
        }
    }
}