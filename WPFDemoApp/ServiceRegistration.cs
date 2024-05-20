namespace WPFDemoApp
{
	public static class ServiceRegistration
    {
		public static IServiceProvider ConfigureServices()
		{
			var services = new ServiceCollection();

			// Register services here
			services.AddDbContext<ApplicationDbContext>(
			options => options.UseSqlite("Data Source=localdatabase.db"));

			services.AddScoped<MainWindow>();
			services.AddScoped<ToDoItem>();
			
			// Add other services
			return services.BuildServiceProvider();
		}
	}
}
