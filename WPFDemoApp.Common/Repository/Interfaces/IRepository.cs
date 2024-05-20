namespace WPFDemoApp.Common.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntityHasBeenDeleted
    {
        Task<ReadOnlyCollection<TEntity>> GetAllDataAsync();
    }
}