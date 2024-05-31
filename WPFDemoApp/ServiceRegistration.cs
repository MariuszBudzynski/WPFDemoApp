using WPFDemoApp.Commands;

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
			services.AddScoped<ISaveSingleDataItemUseCase, SaveSingleDataItemUseCase>();
			services.AddScoped<IGetAllDataUseCase, GetAllDataUseCase>();
			services.AddScoped<ISoftDeleteItemUseCase, SoftDeleteItemUseCase>();
			services.AddScoped<IUpdateDataUseCase, UpdateDataUseCase>();
			services.AddScoped<MainViewModel>();
			services.AddScoped<AddDataCommand>();
			services.AddScoped<DeleteDataCommand>();
			services.AddScoped<SearchCommand>();
			services.AddScoped<UpdateCheckboxCommand>();
			services.AddScoped<FilterComboBoxCommand>();
			services.AddScoped<TextBoxKeyDownCommand>();

			// Add other services
			return services.BuildServiceProvider();
		}
	}
}
