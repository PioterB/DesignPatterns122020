using System;

namespace PbLab.DesignPatterns.Reporting
{
    internal class StatsBuilder
    {
        private readonly string _file;
        private TimeSpan _duration;
        private uint _count;

        public StatsBuilder(string file)
        {
            _file = file;
        }

        public SamplesReadStatistics Build()
        {
            return new SamplesReadStatistics(_file, _duration, _count);
        }

        public void AddDuration(TimeSpan duration)
        {
            _duration = duration;
        }

        public void AddCount(uint samplesCount)
        {
            _count = samplesCount;
        }
    }
}