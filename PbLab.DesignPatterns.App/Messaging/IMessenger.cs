namespace PbLab.DesignPatterns.Messaging
{
    public interface IMessenger
    {
        void Publish(string message);

        void Subscribe(ISubscriber subscriber);

        void Unsubscribe(ISubscriber subscriber);

    }
}