using System.Reflection.Metadata;
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
		private readonly UpdateCheckboxCommand _updateCheckboxCommand;
		private readonly FilterComboBoxCommand _filterComboBoxCommand;
		private readonly TextBoxKeyDownCommand _textBoxKeyDownCommand;

		public MainWindow(MainViewModel viewModel,
				AddDataCommand addDataCommand,
				DeleteDataCommand deleteDataCommand,
				SearchCommand searchCommand,
				UpdateCheckboxCommand updateCheckboxCommand,
				FilterComboBoxCommand filterComboBoxCommand,
				TextBoxKeyDownCommand textBoxKeyDownCommand)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
			_addDataCommand = addDataCommand;
			_deleteDataCommand = deleteDataCommand;
			_searchCommand = searchCommand;
			_updateCheckboxCommand = updateCheckboxCommand;
			_filterComboBoxCommand = filterComboBoxCommand;
			_textBoxKeyDownCommand = textBoxKeyDownCommand;
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			_addDataCommand.Execute(InputTextBox.Text);
		}

		private void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.CommandParameter is ToDoItemDTO item)
			{
				_deleteDataCommand.Execute(item);
			}
		}

		private void Search_Box_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				_searchCommand.Execute(textBox.Text);
			}
		}

		private void Search_Box_TextChangedEdit(object sender, TextChangedEventArgs e)
		{
			if (sender is TextBox textBox)
			{
				_searchCommand.Execute(textBox.Text);
			}
		}
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{

			if (sender is CheckBox checkBox)
			{
				if (checkBox.Tag is ToDoItemDTO toDoItem)
				{
					_updateCheckboxCommand.Execute(toDoItem);
				}
			}


		}

		private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
		{
			if (sender is CheckBox checkBox)
			{
				if (checkBox.Tag is ToDoItemDTO toDoItem)
				{
					_updateCheckboxCommand.Execute(toDoItem);
				}
			}
		}

		private void FilterComboBox_SelectionChanged(object sender, RoutedEventArgs e)
		{
			if (_viewModel == null) return;

			if (sender is ComboBox comboBox)
			{
				_filterComboBoxCommand.Execute(comboBox);
			}
		}

		private void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			_textBoxKeyDownCommand.Execute(e);
		}

		private async void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (e.Source is TabControl tabControl && tabControl.SelectedItem != null)
			{
				if (tabControl.SelectedItem == MainTabControl.Items[0])
				{
					await _viewModel.RefreshDataAsync();
					Search_Box.Text = "";
				}
				else if (tabControl.SelectedItem == MainTabControl.Items[1])
					await _viewModel.RefreshDataAsync();
				Search_BoxEdit.Text = "";
			}
		}
	}
}