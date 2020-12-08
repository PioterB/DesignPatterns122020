using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PbLab.DesignPatterns.Audit
{
    public class LoggerFactory
    {
        private readonly ILogger _composedLogger = new ComposedLogger();

        private readonly IDictionary<string, Type> _decoratorsMap =
            new Dictionary<string, Type>()
            {
                {"time", typeof(TimeStampDecorator)},
                {"thread", typeof(ThreadDecorator)},
                {"domain", typeof(AppDomainDecorator)},
            };

        public ILogger Create(params string[] decorators)
        {
            var result = _composedLogger;

            if (decorators == null || decorators.Length == 0)
            {
                return result;
            }

            var unknown = decorators.Except(_decoratorsMap.Keys).ToArray();
            if (unknown.Any())
            {
                throw new ArgumentOutOfRangeException("invalid decorator name: " + string.Join(",", unknown));
            }

            var fromEnd = decorators.Reverse();


            foreach (var decorator in fromEnd)
            {
               
                result = FactorizeDecorator(result, _decoratorsMap[decorator]);
            }

            return result;
        }

        /*
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
        */
        private static ILogger FactorizeDecorator(ILogger result, Type decoratorType)
        {
            result = (ILogger) Activator.CreateInstance(decoratorType, BindingFlags.CreateInstance, null, result);
            return result;
        }
    }
}