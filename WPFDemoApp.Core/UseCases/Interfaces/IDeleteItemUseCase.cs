namespace WPFDemoApp.Core.UseCases.Interfaces
{
    public interface IDeleteItemUseCase
    {
        Task ExecuteAsync<TEntity>(TEntity textContent) where TEntity : class, IEntityHasBeenCompleted, IEntityDataID;

	}
}