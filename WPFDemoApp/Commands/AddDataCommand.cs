using System.Windows.Input;

public class AddDataCommand : ICommand
{
	private readonly MainViewModel _viewModel;

	public AddDataCommand(MainViewModel viewModel)
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
		if (parameter is string text && !string.IsNullOrEmpty(text))
		{
			var newItem = new ToDoItemDTO(Guid.Empty, text);
			try
			{
				await _viewModel.AddDataAsync(newItem);
			}
			catch (Exception ex)
			{
				MessageBox.Show($"An error occurred while adding data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}
		else
		{
			MessageBox.Show("The input cannot be empty. Please enter some text.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
		}
	}
}