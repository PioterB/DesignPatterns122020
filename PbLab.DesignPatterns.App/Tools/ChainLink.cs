using System;

namespace PbLab.DesignPatterns.Tools
{
    public class ChainLink<TValue> : IChainAction<TValue>
    {
        private readonly ChainLink<TValue> _next;
        private readonly IChainAction<TValue> _action;

        public ChainLink(bool result)
        {
            _action = new GenericChainAction<TValue>(_ => result);
        }

        public ChainLink(IChainAction<TValue> action, ChainLink<TValue> next)
        {
            _next = next;
            _action = action;
        }

        public ChainLink(IChainAction<TValue> value) 
            : this(value, null)
        {
        }

        public ChainLink(Func<TValue, bool> action, ChainLink<TValue> next) 
            : this(new GenericChainAction<TValue>(action), next)
        {
        }

        public ChainLink(Func<TValue, bool> action) 
            : this(new GenericChainAction<TValue>(action), null)
        {
        }

        public bool Handle(TValue value)
        {
            var handled = _action.Handle(value);
            return handled && (_next?.Handle(value) ?? false);
        }
    }
}