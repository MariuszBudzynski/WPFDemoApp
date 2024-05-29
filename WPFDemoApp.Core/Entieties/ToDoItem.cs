namespace WPFDemoApp.Core.Entieties
{
	public class ToDoItem : IEntityId,IEntityTextContent,IEntityHasBeenCompleted
	{
		public int Id { get; set; }
		public string TextContent { get; set; }
		public bool HasBeenCompleted { get; set; } = false ;

	}
}
