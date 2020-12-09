using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PbLab.DesignPatterns.Audit;
using PbLab.DesignPatterns.Model;
using PbLab.DesignPatterns.Reporting;

namespace PbLab.DesignPatterns.Services
{
    public class SourcesService
    {
        private readonly LocalFileReaderPool _readersPool;
        private readonly ILogger _logger;
        private readonly IChanelFactory _chanelFactory;

        protected SourcesService()
        {

        }

        public SourcesService(LocalFileReaderPool readersPool, ILogger logger, IChanelFactory chanelFactory)
        {
            _readersPool = readersPool;
            _logger = logger;
            _chanelFactory = chanelFactory;
        }

        public IEnumerable<Sample> ReadAllSources(IEnumerable<string> paths, IScheduler<string, Sample> scheduler)
        {
            var result = scheduler.Schedule(paths, Processing);
            return result;
        }


        private IEnumerable<Sample> Processing(string file)
        {
            IEnumerable<Sample> samples;

            var timer = InitializeTimer();
            var stats = new StatsBuilder(file);
            var extension = new FileInfo(file).Extension.Trim('.');
            var protocol = ExtractProtocol(file);
            var chanel = InitializeChanel(protocol);
            using (var stream = chanel.Connect(file))
            {
                var reader = _readersPool.Borrow(extension);
                timer.Start();
                samples = reader.Read(stream);
                timer.Stop();
                _readersPool.Release(reader);

                stats.AddDuration(timer.Elapsed);
                stats.AddCount((uint)samples.Count());

                timer.Reset();
            }

            _logger.Log(new ReportPrototype(DateTime.Now).Clone(stats.Build()));

            return samples;
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
}