using System.Windows.Controls;
using System.Windows.Input;
using WPFDemoApp.Commands;

namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewModel _viewModel;
		private readonly AddDataCommand _addDataCommand;
		private readonly DeleteDataCommand _deleteDataCommand;
		private readonly SearchCommand _searchCommand;

		public MainWindow(MainViewModel viewModel,
				AddDataCommand addDataCommand,
				DeleteDataCommand deleteDataCommand,
				SearchCommand searchCommand)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
			_addDataCommand = addDataCommand;
			_deleteDataCommand = deleteDataCommand;
			_searchCommand = searchCommand;
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			_addDataCommand.Execute(InputTextBox.Text);
		}

		private  void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.CommandParameter is ToDoItemDTO item)
			{
				_deleteDataCommand.Execute(item);
			}
		}

		private  void Search_Box_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				_searchCommand.Execute(textBox.Text);
			}
		}

		private async void Search_Box_TextChangedEdit(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				_searchCommand.Execute(textBox.Text);
			}
		}
		//przeniesc do command
		private async void CheckBox_Checked(object sender, RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox)
			{
				if (checkBox.Tag is ToDoItemDTO toDoItem)
				{
					await _viewModel.UpdateCheckbox(toDoItem);
				}
			}
		}

		//przeniesc do command
		private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox)
			{
				if (checkBox.Tag is ToDoItemDTO toDoItem)
				{
					await _viewModel.UpdateCheckbox(toDoItem);
				}
			}
		}

		//przeniesc do command
		private async void FilterComboBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			if (_viewModel == null) return;
			if (sender is ComboBox comboBox)
			{
				var selectedFilter = comboBox.SelectedItem as ComboBoxItem;
				string filter = selectedFilter?.Content.ToString();
				_viewModel.Filter = filter;
				await _viewModel.FilterItems();
			}
		}

		//przeniesc do command
		private async void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (sender is TextBox textBox && textBox.DataContext is ToDoItemDTO toDoItem)
				{
					if (string.IsNullOrWhiteSpace(textBox.Text))
					{
						MessageBox.Show("Text cannot be empty.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
					}
					else
					{
						await _viewModel.UpdateData(toDoItem);
					}
				}
			}
		}

		private async void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.Source is TabControl tabControl && tabControl.SelectedItem != null)
			{
				if (tabControl.SelectedItem == MainTabControl.Items[0]) // Sprawdzamy pierwszą zakładkę
				{
					await _viewModel.RefreshDataAsync();
					Search_Box.Text = ""; // Zresetuj pole wyszukiwania na pierwszej zakładce
				}
				else if (tabControl.SelectedItem == MainTabControl.Items[1]) // Sprawdzamy drugą zakładkę
				{
					await _viewModel.RefreshDataAsync();
					Search_BoxEdit.Text = ""; // Zresetuj pole wyszukiwania na drugiej zakładce
				}
			}
		}



	}
}
