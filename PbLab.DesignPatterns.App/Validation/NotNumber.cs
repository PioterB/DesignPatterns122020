using System.Linq;
using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.Validation
{
    public class NotNumber : IChainAction<string>
    {
        public bool Handle(string value)
        {
            return value.Intersect(new[] {'a', 'b', 'c'}).Any();
        }
    }
}