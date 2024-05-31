using System;
using System.Windows.Input;

namespace WPFDemoApp.Commands
{
	public class SearchCommand : ICommand
	{
		private readonly MainViewModel _viewModel;

		public SearchCommand(MainViewModel viewModel)
		{
			_viewModel = viewModel;
		}

		public event EventHandler CanExecuteChanged;

		public bool CanExecute(object parameter)
		{
			return true;
		}

		public async void Execute(object parameter)
		{
			if (parameter is string searchText)
			{
				try
				{
					await _viewModel.SeaarchAsync(searchText);
				}
				catch (Exception ex)
				{
					MessageBox.Show($"An error occurred while searching: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}
			}
		}
	}
}
