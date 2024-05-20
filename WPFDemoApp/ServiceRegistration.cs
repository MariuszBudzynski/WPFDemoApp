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
			services.AddScoped<IRepository<ToDoItem>, Repository<ToDoItem>>();
			services.AddScoped<IGetAllDataUseCase<ToDoItem>, GetAllDataUseCase<ToDoItem>>();

			// Add other services
			return services.BuildServiceProvider();
		}
	}
}
