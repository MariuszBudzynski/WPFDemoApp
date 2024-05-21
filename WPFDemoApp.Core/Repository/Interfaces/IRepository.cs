namespace WPFDemoApp.Core.Repository.Interfaces
{
	public interface IRepository<TEntity> where TEntity : class, IEntityHasBeenDeleted
	{
		Task<IEnumerable<TEntity>> GetAllDataAsync();
	}
}