namespace WPFDemoApp.Core.Repository.Interfaces
{
	public interface IRepository
	{
		Task<IEnumerable<TEntity>> GetAllDataAsync<TEntity>() where TEntity : class;
		Task SaveSingleDataItem<TEntity>(TEntity data) where TEntity : class, IEntityTextContent;
		Task DeleteItem<TEntity>(TEntity data) where TEntity : class, IEntityTextContent, IEntityHasBeenCompleted;
		Task UpdateItem<TEntity>(TEntity data) where TEntity : class, IEntityTextContent, IEntityHasBeenCompleted;
	}
}