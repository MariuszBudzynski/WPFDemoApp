namespace WPFDemoApp.Entieties
{
	public class ToDoItems : IEntity
	{
        public Guid Id { get; set; }
		public int TextContent { get; set; }
		public bool HasBeenDeleted { get; set; }

	}
}
