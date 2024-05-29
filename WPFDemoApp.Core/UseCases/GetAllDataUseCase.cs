namespace WPFDemoApp.Core.UseCases
{
	public class GetAllDataUseCase : IGetAllDataUseCase
	{
		private readonly IRepository _repository;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		public GetAllDataUseCase(IRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<TEntity>> ExecuteAsync<TEntity>() where TEntity : class
		{
			try
			{
				return await _repository.GetAllDataAsync<TEntity>();
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while processing data.");
				throw;
			}
		}

	}
}
