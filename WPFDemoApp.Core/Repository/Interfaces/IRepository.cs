namespace WPFDemoApp.Core.Repository.Interfaces
{
	public interface IRepository
	{
		Task<IEnumerable<TEntity>> GetAllDataAsync<TEntity>() where TEntity : class, IEntityHasBeenDeleted;

	}
}