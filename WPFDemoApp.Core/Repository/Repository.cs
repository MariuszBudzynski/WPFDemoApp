namespace WPFDemoApp.Core.Repository
{
	public class Repository : IRepository
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly ApplicationDbContext _context;
	
		public Repository(ApplicationDbContext context)
		{
			_context = context;
		}

		public async Task<IEnumerable<TEntity>> GetAllDataAsync<TEntity>() where TEntity : class,IEntityHasBeenDeleted
		{
			try
			{
				IEnumerable<TEntity> data = await _context.Set<TEntity>()
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

		public async Task SaveSingleDataItem<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted,IEntityTextContent
		{

			var existingdata =	await _context.Set<TEntity>()
								.Where(x => x.HasBeenDeleted == false && x.TextContent == data.TextContent)
								.ToListAsync();

			if (existingdata.Count >= 1) return;
			

			try
			{
				_context.Add<TEntity>(data);
				await _context.SaveChangesAsync();
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while saving data to the database.");
				throw;
			}
		}

		public async Task SoftDeleteItem<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted,IEntityTextContent
		{
			var itemExists = await _context.Set<TEntity>().SingleOrDefaultAsync(x=>x.TextContent == data.TextContent);
			if (itemExists == null) return;

			try
			{
				itemExists.HasBeenDeleted = true;
				_context.Update(itemExists);
				await _context.SaveChangesAsync();
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while soft delete.");
				throw;
			}
		}
	}

}