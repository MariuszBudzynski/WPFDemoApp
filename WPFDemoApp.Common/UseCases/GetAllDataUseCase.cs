namespace WPFDemoApp.Common.UseCases
{
	public class GetAllDataUseCase<TEntity> : IGetAllDataUseCase<TEntity> where TEntity : class, IEntityHasBeenDeleted
	{
		private readonly IRepository<TEntity> _repository;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public GetAllDataUseCase(IRepository<TEntity> repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<TEntity>> ExecuteAsync()
		{
			try
			{
				return await _repository.GetAllDataAsync();
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while processing data.");
				throw;
			}
		}
	}
}
