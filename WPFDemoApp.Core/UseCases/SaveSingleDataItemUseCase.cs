namespace WPFDemoApp.Core.UseCases
{
    public class SaveSingleDataItemUseCase : ISaveSingleDataItemUseCase
	{
		private readonly IRepository _repository;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		public SaveSingleDataItemUseCase(IRepository repository)
		{
			_repository = repository;
		}

		public async Task ExecuteAsync<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted
		{
			try
			{
				await _repository.SaveSingleDataItem<TEntity>(data);
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while processing data.");
				throw;
			}
		}
	}
}
