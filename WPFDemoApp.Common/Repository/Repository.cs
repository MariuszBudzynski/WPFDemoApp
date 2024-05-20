namespace WPFDemoApp.Common.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IEntityHasBeenDeleted
	{

		private readonly DbSet<TEntity> _entities;

		public Repository(DbContext context)
		{
			_entities = context.Set<TEntity>();
		}

		public async Task<ReadOnlyCollection<TEntity>> GetAllDataAsync()
		{
			List<TEntity> data = await _entities
				.Where(x => x.HasBeenDeleted == false)
				.ToListAsync();

			return new ReadOnlyCollection<TEntity>(data);
		}

	}
}
