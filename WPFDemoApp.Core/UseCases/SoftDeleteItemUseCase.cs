namespace WPFDemoApp.Core.UseCases
{
    public class SoftDeleteItemUseCase : ISoftDeleteItemUseCase
	{
		private readonly IRepository _repository;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public SoftDeleteItemUseCase(IRepository repository)
		{
			_repository = repository;
		}

		public async Task ExecuteAsync<TEntity>(TEntity textContent) where TEntity : class, IEntityHasBeenDeleted, IEntityTextContent
		{
			try
			{
				await _repository.SoftDeleteItem<TEntity>(textContent);

			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while processing data.");
				throw;
			}
		}
	}
}
