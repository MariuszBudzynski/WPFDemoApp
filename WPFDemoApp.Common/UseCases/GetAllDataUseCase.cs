namespace WPFDemoApp.Common.UseCases
{
    public class GetAllDataUseCase<TEntity> : IGetAllDataUseCase<TEntity> where TEntity : class, IEntityHasBeenDeleted
	{
		private readonly IRepository<TEntity> _repository;
		GetAllDataUseCase(Repository<TEntity> repository)
		{
			_repository = repository;
		}

		public async Task<ReadOnlyCollection<TEntity>> ExecuteAsync()
		{
				return await _repository.GetAllDataAsync();
		}
	}
}
