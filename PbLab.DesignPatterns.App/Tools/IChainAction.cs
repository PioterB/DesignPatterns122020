namespace PbLab.DesignPatterns.Tools
{
    public interface IChainAction<in TValue>
    {
        bool Handle(TValue value);
    }
}