namespace WPFDemoApp.Context
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
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
			optionsBuilder.UseSqlite("Data Source=localdatabase.db");

			return new ApplicationDbContext(optionsBuilder.Options);
		}
	}
}
