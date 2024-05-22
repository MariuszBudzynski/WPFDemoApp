namespace WPFDemoApp.Helpers
{
	public static class ToDoItemMapper
	{
		public static ToDoItemDTO ToDto(this ToDoItem item)
		{
			return new ToDoItemDTO(item.Id, item.TextContent);
		}

		public static IEnumerable<ToDoItemDTO> ToDto(this IEnumerable<ToDoItem> items)
		{
			return items.Select(item => item.ToDto());
		}
	}
}
