using System;

namespace PbLab.DesignPatterns.Audit
{
    internal class AppDomainDecorator : ILogger
    {
        private readonly ILogger _inner;

        public AppDomainDecorator(ILogger inner)
        {
            _inner = inner;
        }

        public void Log(string message)
        {
            var domain = CurrentAppDomain();
            _inner.Log($"[{domain}]  {message}");
        }

        private string CurrentAppDomain()
        {
            return AppDomain.CurrentDomain.Id.ToString();
        }
    }
}