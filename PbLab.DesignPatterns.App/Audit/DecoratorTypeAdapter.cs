using System;
using System.Collections.Generic;
using System.Linq;

namespace PbLab.DesignPatterns.Audit
{
    public class DecoratorTypeAdapter
    {
        private readonly IDictionary<string, Type> _decoratorsMap =
            new Dictionary<string, Type>()
            {
                {"time", typeof(TimeStampDecorator)},
                {"thread", typeof(ThreadDecorator)},
                {"domain", typeof(AppDomainDecorator)},
            };

        public Type FromString(string name)
        {
            if (_decoratorsMap.ContainsKey(name) == false)
            {
                throw new ArgumentOutOfRangeException("invalid decorator name: " + name);
            }

            return _decoratorsMap[name];
        }

        public IEnumerable<Type> FromString(params string[] names)
        {
            var unknown = names.Except(_decoratorsMap.Keys).ToArray();
            if (unknown.Any())
            {
                throw new ArgumentOutOfRangeException("invalid decorator name: " + string.Join(",", unknown));
            }

            return names.Select(name => FromString(name)).ToArray();
        }
    }
}