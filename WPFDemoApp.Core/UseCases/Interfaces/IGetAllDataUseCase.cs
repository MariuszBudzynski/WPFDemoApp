namespace WPFDemoApp.Core.UseCases.Interfaces
{
	public interface IGetAllDataUseCase
	{
		Task<IEnumerable<ToDoItem>> ExecuteAsync();
	}
}