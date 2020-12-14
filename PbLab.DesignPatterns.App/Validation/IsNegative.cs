using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.Validation
{
    public class IsNegative : IChainAction<string>
    {
        public bool Handle(string value)
        {
            return value.Trim().StartsWith("-");
        }
    }
}