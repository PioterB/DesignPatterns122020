using System.Windows;
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
			DataContext = new MainWindowViewModel();
		}
	}
}
