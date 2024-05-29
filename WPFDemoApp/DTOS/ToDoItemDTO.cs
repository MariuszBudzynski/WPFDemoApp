namespace WPFDemoApp.DTOS
{
	public record ToDoItemDTO(string TextContent,bool HasBeenCompleted = false);
}
