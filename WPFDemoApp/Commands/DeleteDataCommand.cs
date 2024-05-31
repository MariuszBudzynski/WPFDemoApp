using System;
using System.Windows.Input;

namespace WPFDemoApp.Commands
{
	public class DeleteDataCommand : ICommand
	{
		private readonly MainViewModel _viewModel;

		public DeleteDataCommand(MainViewModel viewModel)
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
			if (parameter is ToDoItemDTO item)
			{
				try
				{
					await _viewModel.DeleteDataAsync(item);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"An error occurred while deleting data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	}
}
