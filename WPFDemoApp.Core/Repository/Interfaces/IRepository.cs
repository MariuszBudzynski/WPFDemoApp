namespace WPFDemoApp.Core.Repository.Interfaces
{
	public interface IRepository
	{
		Task<IEnumerable<TEntity>> GetAllDataAsync<TEntity>() where TEntity : class, IEntityHasBeenDeleted;
		Task SaveSingleDataItem<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted;
		Task SoftDeleteItem<TEntity>(TEntity data) where TEntity : class, IEntityHasBeenDeleted, IEntityTextContent;
	}
}