using System;

namespace PbLab.DesignPatterns.Tools
{
    public class GenericChainAction<TValue> : IChainAction<TValue>
    {
        private readonly Func<TValue, bool> _action;

        public GenericChainAction(Func<TValue, bool> action)
        {
            _action = action;
        }

        public bool Handle(TValue value)
        {
            return _action(value);
        }
    }
}