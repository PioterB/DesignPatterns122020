using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace PbLab.DesignPatterns.Audit
{
    public class LoggerFactory
    {
        private readonly DecoratorTypeAdapter _decoratorAdapter;
        private readonly ILogger _composedLogger = new ComposedLogger();

        public LoggerFactory(DecoratorTypeAdapter decoratorAdapter)
        {
            _decoratorAdapter = decoratorAdapter;
        }

        public ILogger Create(params string[] decorators)
        {
            var result = _composedLogger;

            if (decorators == null || decorators.Length == 0)
            {
                return result;
            }

            var fromEnd = decorators.Reverse().ToArray();

            foreach (var decorator in _decoratorAdapter.FromString(fromEnd))
            {
                result = FactorizeDecorator(result, decorator);
            }

            return result;
        }

        
        public ILogger Create(params Type[] decorators)
        {
            var result = _composedLogger;

            if (decorators == null || decorators.Length == 0)
            {
                return result;
            }

            var fromEnd = decorators.Reverse();
            
            foreach (var decoratorType in fromEnd)
            {
                result = FactorizeDecorator(result, decoratorType);
            }

            return _composedLogger;
        }
        
        private static ILogger FactorizeDecorator(ILogger result, Type decoratorType)
        {
            result = (ILogger) Activator.CreateInstance(decoratorType, BindingFlags.CreateInstance, null,  new[] {result});
            return result;
        }
    }
}