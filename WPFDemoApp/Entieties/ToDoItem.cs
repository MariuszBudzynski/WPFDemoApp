namespace WPFDemoApp.Entieties
{
	public class ToDoItem : IEntity
	{
        public int Id { get; set; }
		public string TextContent { get; set; }
		public bool HasBeenDeleted { get; set; }

	}
}
