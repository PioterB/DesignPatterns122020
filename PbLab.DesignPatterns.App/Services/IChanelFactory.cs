namespace PbLab.DesignPatterns.Services
{
    public interface IChanelFactory
    {
        IChanel Create(string protocol);
    }
}