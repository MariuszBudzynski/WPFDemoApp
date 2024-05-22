namespace WPFDemoApp.Core.Context
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
		: base(options)
		{
		}
		public ApplicationDbContext() { }
		public DbSet<ToDoItem> ToDoItems { get; set; }
	}

	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		//this is needed due to the WPF project
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var _configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();

			var connectionString = _configuration.GetConnectionString("ToDoItems");

			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlServer(connectionString);
			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}
