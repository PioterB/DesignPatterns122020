using System.Collections.Generic;

namespace PbLab.DesignPatterns.Messaging
{
    public class Messenger : IMessenger
    {
        private readonly ISet<ISubscriber> _listeners = new HashSet<ISubscriber>();

        public void Subscribe(ISubscriber subscriber)
        {
            _listeners.Add(subscriber);
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _listeners.Remove(subscriber);
        }

        public void Publish(string message)
        {
            foreach (var subscriber in _listeners)
            {
                subscriber.Notify(message);
            }
        }
    }
}