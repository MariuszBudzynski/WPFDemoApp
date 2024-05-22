namespace WPFDemoApp
{
	public static class ServiceRegistration
    {
		public static IServiceProvider ConfigureServices(IConfiguration configuration)
		{
			var services = new ServiceCollection();

			services.AddDbContext<ApplicationDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("ToDoItems")));

			services.AddScoped<MainWindow>();
			services.AddScoped<ToDoItem>();
			services.AddScoped<IRepository, Repository>();
			services.AddScoped<IGetAllDataUseCase, GetAllDataUseCase>();
			services.AddScoped<MainViewModel>();

			// Add other services
			return services.BuildServiceProvider();
		}
	}
}
