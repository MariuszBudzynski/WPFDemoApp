namespace WPFDemoApp.Core.UseCases
{
    public class UpdateDataUseCase : IUpdateDataUseCase
	{
		private readonly IRepository _repository;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public UpdateDataUseCase(IRepository repository)
		{
			_repository = repository;
		}

		public async Task ExecuteAsync<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted, IEntityTextContent, IEntityHasBeenCompleted
		{
			try
			{
				await _repository.UpdateItem(data);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while processing data.");
				throw;
			}
		}

	}
}
