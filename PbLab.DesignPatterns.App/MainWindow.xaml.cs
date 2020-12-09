using System;
using System.Windows;
using PbLab.DesignPatterns.Audit;
using PbLab.DesignPatterns.Model;
using PbLab.DesignPatterns.Services;
using PbLab.DesignPatterns.ViewModels;

namespace PbLab.DesignPatterns
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

            IScheduler<string, Sample> scheduler = Environment.ProcessorCount == 1
                ? (IScheduler<string, Sample>)new LinearScheduler<string, Sample>()
                : new TasksScheduler<string, Sample>();

			var loggerFactory = new LoggerFactory(new DecoratorTypeAdapter());
            var readerPoll = new LocalFileReaderPool(new LocalFileReaderFactory());
            //var sourcesService = new SourcesService(readerPoll, loggerFactory.Create("time"), new ChanelFactory());
            var sourcesService = new StrictSourcesService();

            DataContext = new MainWindowViewModel(loggerFactory, sourcesService, scheduler);
        }
	}
}
