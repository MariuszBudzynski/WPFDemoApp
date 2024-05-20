using Microsoft.Extensions.DependencyInjection;

namespace WPFDemoApp
{
	public static class ServiceRegistration
    {
		public static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();

			// Register services here
			services.AddScoped<MainWindow>();


			// Add other services
			return services.BuildServiceProvider();
		}
	}
}
