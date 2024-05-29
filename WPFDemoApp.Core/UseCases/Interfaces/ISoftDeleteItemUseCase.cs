namespace WPFDemoApp.Core.UseCases.Interfaces
{
    public interface ISoftDeleteItemUseCase
    {
        Task ExecuteAsync<TEntity>(TEntity textContent) where TEntity : class, IEntityHasBeenDeleted, IEntityTextContent, IEntityHasBeenCompleted;
    }
}