namespace WPFDemoApp.DTOS
{
	public record ToDoItemDTO(Guid DataId,string TextContent,bool HasBeenCompleted = false);
}
