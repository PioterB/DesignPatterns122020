using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PbLab.DesignPatterns.Audit;
using PbLab.DesignPatterns.Messaging;
using PbLab.DesignPatterns.Model;
using PbLab.DesignPatterns.Reporting;

namespace PbLab.DesignPatterns.Services
{
    public class SourcesService
    {
        private readonly LocalFileReaderPool _readersPool;
        private readonly ILogger _logger;
        private readonly IChanelFactory _chanelFactory;
        private readonly IScheduler<string, Sample> _defaultScheduler;

        protected SourcesService(IScheduler<string, Sample> defaultScheduler)
        {
            _defaultScheduler = defaultScheduler;
        }

        public SourcesService(LocalFileReaderPool readersPool, ILogger logger, IChanelFactory chanelFactory, IScheduler<string, Sample> defaultScheduler)
        {
            _readersPool = readersPool;
            _logger = logger;
            _chanelFactory = chanelFactory;
            _defaultScheduler = defaultScheduler;

        }

        public IEnumerable<Sample> ReadAllSources(IEnumerable<string> paths, IScheduler<string, Sample> scheduler)
        {
            scheduler = scheduler ?? _defaultScheduler;
            var result = scheduler.Schedule(paths, Processing);
            return result;
        }


        private IEnumerable<Sample> Processing(string file)
        {
            IEnumerable<Sample> samples;

            var timer = InitializeTimer();
            var extension = GatherFormatType(file);
            var protocol = ExtractProtocol(file);
            var chanel = InitializeChanel(protocol);
            var reader = GetReader(extension);
            using (var stream = chanel.Connect(file))
            {
                timer.Start();
                samples = reader.Read(stream);
                timer.Stop();
            }

            DiscardReader(reader);
            
            var stats = BuildStatistics(file, timer.Elapsed, samples);
            
            Audit(stats);

            return samples;
        }

        protected virtual void Audit(SamplesReadStatistics stats)
        {
            _logger.Log(new ReportPrototype(DateTime.Now).Clone(stats));
        }

        protected virtual SamplesReadStatistics BuildStatistics(string file, TimeSpan duration, IEnumerable<Sample> samples)
        {
            var stats = new StatsBuilder(file);
            stats.AddDuration(duration);
            stats.AddCount((uint) samples.Count());
            return stats.Build();
        }

        protected virtual void DiscardReader(ISamplesReader reader)
        {
            _readersPool.Release(reader);
        }

        protected virtual ISamplesReader GetReader(string extension)
        {
            return _readersPool.Borrow(extension);
        }

        protected virtual string GatherFormatType(string file)
        {
            return new FileInfo(file).Extension.Trim('.');
        }

        protected virtual IChanel InitializeChanel(string protocol)
        {
            return _chanelFactory.Create(protocol);
        }

        protected virtual Stopwatch InitializeTimer()
        {
            return new Stopwatch();
        }

        protected virtual string ExtractProtocol(string file)
        {
            return file.Split(':').First();
        }
    }

    public class StrictSourcesService : SourcesService
    {
        private readonly ILogger _logger;

        public StrictSourcesService(IMessenger messenger) : base(new LinearScheduler<string, Sample>())
        {
            _logger = new ComposedLogger(messenger);
        }
        
        protected override IChanel InitializeChanel(string protocol)
        {
            return new FileChanel();
        }

        protected override string GatherFormatType(string file)
        {
            return file.Split('.').Last();
        }

        protected override ISamplesReader GetReader(string extension)
        {
            return new JsonSamplesReader();
        }

        protected override void DiscardReader(ISamplesReader reader)
        {
        }

        protected override void Audit(SamplesReadStatistics stats)
        {
            _logger.Log(new ReportPrototype(DateTime.Now).Clone(stats));
        }
    }
}