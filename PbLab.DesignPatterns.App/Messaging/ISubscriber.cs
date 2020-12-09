namespace PbLab.DesignPatterns.Messaging
{
    public interface ISubscriber
    {
        void Notify(string message);
    }
}