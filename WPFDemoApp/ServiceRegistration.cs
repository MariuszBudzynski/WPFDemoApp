namespace WPFDemoApp
{
	public static class ServiceRegistration
    {
		public static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();

			// Register services here
			services.AddScoped<MainWindow>();
			services.AddDbContext<ApplicationDbContext>(
			options => options.UseSqlite("Data Source=localdatabase.db"));
			
			// Add other services
			return services.BuildServiceProvider();
		}
	}
}
