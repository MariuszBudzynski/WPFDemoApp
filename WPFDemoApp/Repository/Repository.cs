namespace WPFDemoApp.Repository
{
	public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityHasBeenDeleted
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly DbSet<TEntity> _entities;

		public Repository(ApplicationDbContext context)
		{
			//_entities = context.Set<TEntity>();
		}

		public async Task<IEnumerable<TEntity>> GetAllDataAsync()
		{
			try
			{
				IEnumerable<TEntity> data = await _entities
					.Where(x => x.HasBeenDeleted == false)
					.ToListAsync();

				return data;
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while retrieving data from the database.");
				throw;
			}
		}

	}
}
