namespace WPFDemoApp.Core.Repository.Interfaces
{
	public interface IRepository
	{
		Task<IEnumerable<ToDoItem>> GetAllDataAsync();
	}
}