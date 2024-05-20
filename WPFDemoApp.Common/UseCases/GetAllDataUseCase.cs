namespace WPFDemoApp.Common.UseCases
{
    public class GetAllDataUseCase<TEntity> : IGetAllDataUseCase<TEntity> where TEntity : class
	{
		private readonly IRepository<TEntity> _repository;
		GetAllDataUseCase(Repository<TEntity> repository)
		{
			_repository = repository;
		}

		public async Task<List<TEntity>> ExecuteAsync()
		{
			var entities = await _repository.GetAllDataAsync();
			return await entities.ToListAsync();
		}
	}
}
