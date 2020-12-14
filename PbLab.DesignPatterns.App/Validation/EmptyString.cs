using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.Validation
{
    public class EmptyString : IChainAction<string>
    {
        public bool Handle(string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}