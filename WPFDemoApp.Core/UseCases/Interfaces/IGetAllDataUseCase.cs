namespace WPFDemoApp.Core.UseCases.Interfaces
{
	public interface IGetAllDataUseCase<TEntity> where TEntity : class, IEntityHasBeenDeleted
	{
		Task<IEnumerable<TEntity>> ExecuteAsync();
	}
}