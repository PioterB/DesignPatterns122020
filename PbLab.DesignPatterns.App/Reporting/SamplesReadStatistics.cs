using System;

namespace PbLab.DesignPatterns.Reporting
{
    internal class SamplesReadStatistics
    {
        public string File { get; }

        public TimeSpan Duration { get; }

        public uint Count { get; }

        public SamplesReadStatistics(string file, TimeSpan duration, uint count)
        {
            File = file;
            Duration = duration;
            Count = count;
        }
    }
}