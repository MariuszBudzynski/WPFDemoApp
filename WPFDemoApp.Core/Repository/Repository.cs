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
				var items = await _context.Set<TEntity>()
								.Where(x => x.TextContent == data.TextContent)
								.ToListAsync();

			var existingdata = items.FirstOrDefault(x => x.HasBeenDeleted == false);

			var deletedData = items.FirstOrDefault(x => x.HasBeenDeleted == true);


			if (existingdata != null)
			{
				return;
			}
			 if (deletedData!= null)
			{
					try
					{
						deletedData.HasBeenDeleted = false;
						_context.Update(deletedData);
						await _context.SaveChangesAsync();
					}
					catch (DbException ex)
					{
						_logger.Error(ex, "An error occurred while saving data to the database.");
						throw;
					}
					return;
			}


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

		public async Task SoftDeleteItem<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted,IEntityTextContent, IEntityHasBeenCompleted
		{
			var itemExists = await _context.Set<TEntity>().SingleOrDefaultAsync(x=>x.TextContent == data.TextContent);
			if (itemExists == null) return;

			try
			{
				itemExists.HasBeenDeleted = true;
				await UpdateItem(itemExists);

			}
			catch (DbException ex)
			{
				_logger.Error(ex, "An error occurred while soft delete was in progress.");
				throw;
			}
		}

		public async Task UpdateItem<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted, IEntityTextContent, IEntityHasBeenCompleted
		{
			
			if (data == null) return;
 
            try
			{
				_context.Update(data);
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