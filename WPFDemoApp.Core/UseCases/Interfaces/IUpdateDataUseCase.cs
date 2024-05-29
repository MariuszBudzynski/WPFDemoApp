namespace WPFDemoApp.Core.UseCases.Interfaces
{
    public interface IUpdateDataUseCase
    {
        Task ExecuteAsync<TEntity>(TEntity data) where TEntity : class, IEntityTextContent, IEntityHasBeenCompleted;
    }
}