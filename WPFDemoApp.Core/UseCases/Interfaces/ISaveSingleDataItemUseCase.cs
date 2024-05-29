namespace WPFDemoApp.Core.UseCases.Interfaces
{
    public interface ISaveSingleDataItemUseCase
    {
        Task ExecuteAsync<TEntity>(TEntity data) where TEntity : class, IEntityTextContent;
    }
}