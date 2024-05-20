namespace WPFDemoApp.Common.UseCases.Interfaces
{
    public interface IGetAllDataUseCase<TEntity> where TEntity : class, IEntityHasBeenDeleted, IEntityTextContent
	{
        Task<IEnumerable<TEntity>> ExecuteAsync();
    }
}