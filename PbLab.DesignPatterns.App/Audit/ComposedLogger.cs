using System;
using PbLab.DesignPatterns.Messaging;

namespace PbLab.DesignPatterns.Audit
{
    internal class ComposedLogger : ILogger
    {
        private readonly IMessenger _messenger;

        public ComposedLogger(IMessenger messenger)
        {
            _messenger = messenger;
        }

        public void Log(string message)
        {
            Write(message);
        }

        private void Write(string message)
        {
            _messenger.Publish(message);
        }
    }
}