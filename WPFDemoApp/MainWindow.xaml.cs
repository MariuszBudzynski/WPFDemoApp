using System.Windows.Controls;

namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewModel _viewModel;

		public MainWindow(MainViewModel viewModel, ISaveSingleDataItemUseCase saveSingleDataItemUseCase)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
		}

		private async void AddButton_Click(object sender, RoutedEventArgs e)
		{
			await _viewModel.AddDataAsync(InputTextBox.Text);
		
		}

		private async void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.CommandParameter is string item)
			{

				await _viewModel.DeleteDataAsync(item);
			}

		}

		private async void Search_Box_TextChanged(object sender, TextChangedEventArgs e)
		{
			string searchText = Search_Box.Text;

			await _viewModel.SeaarchAsync(searchText);

		}

		private async void CheckBox_Checked(object sender,RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox)
			{
				if (checkBox.Tag is string toDoItem)
				{
					await _viewModel.UpdateCheckbox(toDoItem, checkBox.IsChecked ?? false);
				}
			}
		}

	}
}