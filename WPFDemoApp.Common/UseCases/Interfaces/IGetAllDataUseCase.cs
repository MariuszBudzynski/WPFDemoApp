namespace WPFDemoApp.Common.UseCases.Interfaces
{
    public interface IGetAllDataUseCase<TEntity> where TEntity : class
    {
        Task<ReadOnlyCollection<TEntity>> ExecuteAsync();
    }
}