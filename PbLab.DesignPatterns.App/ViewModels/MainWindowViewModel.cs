using System;
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
using PbLab.DesignPatterns.Messaging;
using PbLab.DesignPatterns.Model;
using PbLab.DesignPatterns.Reporting;
using PbLab.DesignPatterns.Services;
using PbLab.DesignPatterns.Tools;

namespace PbLab.DesignPatterns.ViewModels
{
	public class MainWindowViewModel : ViewModelBase, ISubscriber
    {
        private readonly ObservableCollection<string> _selectedFiles = new ObservableCollection<string>();
		private readonly ObservableCollection<Sample> _samples = new ObservableCollection<Sample>();
		private readonly ObservableCollection<string> _logs = new ObservableCollection<string>();
        private readonly ILogger _logger;
        private readonly SourcesService _samplesService;
        private readonly IScheduler<string, Sample> _scheduler;
        private readonly IMessenger _messenger;

        public MainWindowViewModel(LoggerFactory loggerFactory, SourcesService samplesService, IScheduler<string, Sample> scheduler, IMessenger messenger)
		{
            _samplesService = samplesService;
            _scheduler = scheduler;
            _messenger = messenger;
            //_logger = loggerFactory.Create(typeof(TimeStampDecorator), typeof(ThreadDecorator));
			_logger = loggerFactory.Create("time", "thread");

			messenger.Subscribe(this);

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
            _samples.Clear();


            var samples = _samplesService.ReadAllSources(_selectedFiles, null);

            //var samples = SourceReader.ReadAllSources(_selectedFiles, _readerFactory, _logger, new ChanelFactory());
			samples.ToList().ForEach(s => _samples.Add(s));
            
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

        public void Notify(string message)
        {
			_logs.Add(message);
        }
    }
}