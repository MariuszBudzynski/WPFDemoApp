namespace WPFDemoApp.Common.Repository.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IQueryable<TEntity>> GetAllDataAsync();
    }
}