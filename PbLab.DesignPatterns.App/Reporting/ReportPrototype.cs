using System;
using System.Text;

namespace PbLab.DesignPatterns.Reporting
{
    internal class ReportPrototype
    {
        private readonly StringBuilder _template;

        public ReportPrototype(DateTime timeStamp, string template = null)
        {
            _template = new StringBuilder(template ?? LoadTemplateFromResources());
            _template.Replace("{timestamp}", timeStamp.ToString());
        }

        public string Clone(SamplesReadStatistics stats)
        {
            _template.Replace("{count}", stats.Count.ToString());
            _template.Replace("{duration}", stats.Duration.ToString());
            _template.Replace("{location}", stats.File);

            return _template.ToString();
        }

        private string LoadTemplateFromResources()
        {
            return "{timestamp} loaded {count} in {duration} from {location}";
        }
    }
}