namespace WPFDemoApp.Entieties
{
	public class ToDoItem : IEntityId, IEntityHasBeenDeleted, IEntityTextContent
	{
        public int Id { get; set; }
		public string TextContent { get; set; }
		public bool HasBeenDeleted { get; set; } = false;

	}
}
