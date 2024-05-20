using NLog;

namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IServiceProvider _serviceProvider;
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		App()
		{
			_serviceProvider = ServiceRegistration.ConfigureServices();

		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
			mainWindow.Show();
		}
	}

}
