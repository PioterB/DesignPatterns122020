using System.Windows;
using PbLab.DesignPatterns.Audit;
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
			var loggerFactory = new LoggerFactory();

            DataContext = new MainWindowViewModel(new LocalFileReaderPool(new LocalFileReaderFactory()), loggerFactory);
        }
	}
}
