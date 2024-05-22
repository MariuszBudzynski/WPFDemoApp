using Microsoft.Extensions.Configuration;
using System.IO;

namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IServiceProvider _serviceProvider;
		private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
		public IConfiguration Configuration { get; private set; }
		App()
		{


			var builder = new ConfigurationBuilder()
			.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

			Configuration = builder.Build();

			_serviceProvider = ServiceRegistration.ConfigureServices(Configuration);


		}

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);

			var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();



			mainWindow.Show();
		}
	}

}
