﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Markup;
using PbLab.DesignPatterns.Audit;
using PbLab.DesignPatterns.Model;
using PbLab.DesignPatterns.Reporting;
using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.Services
{
    public abstract class SourceReader
    {
        public static IEnumerable<Sample> ReadAllSources(IEnumerable<string> paths, LocalFileReaderPool readersPool, ILogger logger, IChanelFactory chanelFactory)
        {
            var result = new List<Sample>();
            var report = new ReportPrototype(DateTime.Now);
            var reports = new List<string>(paths.Count());
            var timer = new Stopwatch();
            foreach (var file in paths)
            {
                var stats = new StatsBuilder(file);
                var extension = new FileInfo(file).Extension.Trim('.');
                var protocol = ExtractProtocol(file);
                var chanel = chanelFactory.Create(protocol);
                using (var stream = chanel.Connect(file))
                {
                    var reader = readersPool.Borrow(extension);
                    timer.Start();
                    var samples = reader.Read(stream);
                    timer.Stop();
                    readersPool.Release(reader);
                    result.AddRange(samples);

                    stats.AddDuration(timer.Elapsed);
                    stats.AddCount((uint)samples.Count());

                    timer.Reset();
                }

                reports.Add(report.Clone(stats.Build()));
                logger.Log(report.Clone(stats.Build()));
            }

            return result;
        }

        private static string ExtractProtocol(string file)
        {
            return file.Split(':').First();
        }
    }
}