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

		public async Task<IEnumerable<TEntity>> GetAllDataAsync<TEntity>() where TEntity : class
		{
			try
			{
				IEnumerable<TEntity> data = await _context.Set<TEntity>()
					.ToListAsync();

				return data;
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while retrieving data from the database.");
				throw;
			}
		}

		public async Task SaveSingleDataItem<TEntity>(TEntity data) where TEntity : class,IEntityTextContent
		{
	
			var existingData = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.TextContent == data.TextContent);


			if (existingData != null)
			{
				return;
			}

			try
			{

				_context.Add(data);
				await _context.SaveChangesAsync();
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while saving data to the database.");
				throw;
			}
		}

		public async Task DeleteItem<TEntity>(TEntity data) where TEntity : class, IEntityDataID, IEntityHasBeenCompleted
		{
			var item = await _context.Set<TEntity>().SingleOrDefaultAsync(x => x.DataID == data.DataID);
			if (item == null) return;

			try
			{
				_context.Set<TEntity>().Remove(item);
				await _context.SaveChangesAsync();
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while deleting the item.");
				throw;
			}
		}


		public async Task UpdateItem<TEntity>(TEntity data) where TEntity : class, IEntityDataID, IEntityHasBeenCompleted,IEntityTextContent
		{
			
			if (data == null) return;
			var existingData = (await GetAllDataAsync<TEntity>()).FirstOrDefault(x=>x.DataID==data.DataID);


			try
			{
				existingData.TextContent = data.TextContent;
				existingData.HasBeenCompleted = data.HasBeenCompleted;
				_context.Update(existingData);
				await _context.SaveChangesAsync();
			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while updating database.");
				throw;
			}
		}
	}

}