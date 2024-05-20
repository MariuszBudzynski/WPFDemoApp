namespace WPFDemoApp.Common.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
	{

		private DbSet<TEntity> _entities;

		public Repository(DbContext context)
		{
			_entities = context.Set<TEntity>();
		}

		public async Task<IQueryable<TEntity>> GetAllDataAsync()
		{
			return await Task.FromResult(_entities.AsQueryable());
		}

	}
}
