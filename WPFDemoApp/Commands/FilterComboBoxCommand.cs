namespace WPFDemoApp.Commands
{
	public class FilterComboBoxCommand : ICommand
	{
		private readonly MainViewModel _viewModel;

		public FilterComboBoxCommand(MainViewModel viewModel)
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
			if (parameter is ComboBox comboBox)
			{
				var selectedFilter = comboBox.SelectedItem as ComboBoxItem;
				string filter = selectedFilter?.Content.ToString();
				_viewModel.Filter = filter;
				await _viewModel.FilterItems();
			}

		}
	}
}
