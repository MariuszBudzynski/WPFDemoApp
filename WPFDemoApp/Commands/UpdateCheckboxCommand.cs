public class UpdateCheckboxCommand : ICommand
{
	private readonly MainViewModel _viewModel;

	public UpdateCheckboxCommand(MainViewModel viewModel)
	{
		_viewModel = viewModel;
	}

	public event EventHandler CanExecuteChanged;

	public bool CanExecute(object? parameter)
	{
		return true;
	}

	public async void Execute(object? parameter)
	{
		if (parameter is ToDoItemDTO toDoItem)
		{
			try
			{
				await _viewModel.UpdateCheckbox(toDoItem);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
	}
}
