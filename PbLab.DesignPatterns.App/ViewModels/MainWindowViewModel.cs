﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using Newtonsoft.Json;
using PbLab.DesignPatterns.Audit;
using PbLab.DesignPatterns.Model;
using PbLab.DesignPatterns.Reporting;
using PbLab.DesignPatterns.Services;

namespace PbLab.DesignPatterns.ViewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
        private readonly ISamplesReaderFactory _readerFactory;
        private readonly ObservableCollection<string> _selectedFiles = new ObservableCollection<string>();
		private readonly ObservableCollection<Sample> _samples = new ObservableCollection<Sample>();
		private readonly ObservableCollection<string> _logs = new ObservableCollection<string>();
        private ILogger _logger;

        public MainWindowViewModel(ISamplesReaderFactory readerFactory, LoggerFactory loggerFactory)
		{
            _readerFactory = readerFactory;
            _logger = loggerFactory.Create();
			_logger.NewEntry += message => _logs.Add(message);

            SelectedFiles = new ReadOnlyObservableCollection<string>(_selectedFiles);
			Samples = new ReadOnlyObservableCollection<Sample>(_samples);
			Logs = new ReadOnlyObservableCollection<string>(_logs);
			OpenFileCmd = new RelayCommand(OnOpenFile, CanOpenFile);
			RemoveFileCmd = new RelayCommand<string>(OnRemoveFile, CanRemoveFile);
			ReadFileCmd = new RelayCommand(OnReadFiles, CanReadFiles);
		}

        private bool CanReadFiles() => _selectedFiles.Any();

		private void OnReadFiles()
        {
            var report = new ReportPrototype(DateTime.Now);
			var reports = new List<string>(_selectedFiles.Count);
            var timer = new Stopwatch();
			_samples.Clear();
			foreach (var file in _selectedFiles)
            {
                var stats = new StatsBuilder(file);
                var extension = new FileInfo(file).Extension.Trim('.');
                using (var stream = File.OpenText(file))
                {
					timer.Start();
                    var samples = _readerFactory.Get(extension).Read(stream).ToList();
					timer.Stop();
                    samples.ForEach(s => _samples.Add(s));

                    stats.AddDuration(timer.Elapsed);
                    stats.AddCount((uint)samples.Count);

                    timer.Reset();
                }
				
                reports.Add(report.Clone(stats.Build()));
				_logger.Log(report.Clone(stats.Build()));
            }

			_selectedFiles.Clear();
		}

		private bool CanRemoveFile(string arg) => true;
		private bool CanOpenFile() => true;

		private void OnRemoveFile(string file)
		{
			_selectedFiles.Remove(file);
			ReadFileCmd.RaiseCanExecuteChanged();
		}

		private void OnOpenFile()
		{
			var dialog = new OpenFileDialog();
			var result = dialog.ShowDialog();
			if (result == false)
			{
				return;
			}

			_selectedFiles.Add(dialog.FileName);
			ReadFileCmd.RaiseCanExecuteChanged();
		}

		public ReadOnlyObservableCollection<string> SelectedFiles { get; }

		public ReadOnlyObservableCollection<Sample> Samples { get; }

		public ReadOnlyObservableCollection<string> Logs { get; }

		public RelayCommand OpenFileCmd { get; private set; }

		public RelayCommand<string> RemoveFileCmd { get; private set; }

		public RelayCommand ReadFileCmd { get; private set; }
	}
}