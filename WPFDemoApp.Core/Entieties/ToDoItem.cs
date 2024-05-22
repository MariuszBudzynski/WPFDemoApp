namespace WPFDemoApp.Core.Entieties
{
	public class ToDoItem
	{
		public int Id { get; set; }
		public string TextContent { get; set; }
		public bool HasBeenDeleted { get; set; } = false;

	}
}
