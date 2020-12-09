using System;
using System.Windows;
using System.Windows.Input;
using PbLab.DesignPatterns.Audit;
using PbLab.DesignPatterns.Messaging;
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

			var messenger = new Messenger();

            IScheduler<string, Sample> scheduler = Environment.ProcessorCount == 1
                ? (IScheduler<string, Sample>)new LinearScheduler<string, Sample>()
                : new TasksScheduler<string, Sample>();

			var loggerFactory = new LoggerFactory(new DecoratorTypeAdapter(), messenger);
            var readerPoll = new LocalFileReaderPool(new LocalFileReaderFactory());
            //var sourcesService = new SourcesService(readerPoll, loggerFactory.Create("time"), new ChanelFactory());
            var sourcesService = new StrictSourcesService(messenger);

            DataContext = new MainWindowViewModel(loggerFactory, sourcesService, scheduler, messenger);
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = false;
        }
    }
}
