namespace WPFDemoApp.Core.UseCases.Interfaces
{
	public interface IGetAllDataUseCase
	{
		Task<IEnumerable<TEntity>> ExecuteAsync<TEntity>() where TEntity : class;
	}
}