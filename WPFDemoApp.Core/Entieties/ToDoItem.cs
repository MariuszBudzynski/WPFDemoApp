namespace WPFDemoApp.Core.Entieties
{
	public class ToDoItem : IEntityId,IEntityTextContent,IEntityHasBeenDeleted,IEntityHasBeenCompleted
	{
		public int Id { get; set; }
		public string TextContent { get; set; }
		public bool HasBeenDeleted { get; set; } = false;
		public bool HasBeenCompleted { get; set; } = false ;

	}
}
