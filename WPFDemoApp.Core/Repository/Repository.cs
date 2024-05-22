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

		public async Task<IEnumerable<ToDoItem>> GetAllDataAsync()
		{
			try
			{
				IEnumerable<ToDoItem> data = await _context.ToDoItems
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