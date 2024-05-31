﻿using System.Windows.Controls;
using System.Windows.Input;

namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewModel _viewModel;

		public MainWindow(MainViewModel viewModel)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
		}

		private async void AddButton_Click(object sender, RoutedEventArgs e)
		{
			if (!string.IsNullOrEmpty(InputTextBox.Text))
			{
				var newItem = new ToDoItemDTO(Guid.Empty,InputTextBox.Text);
				await _viewModel.AddDataAsync(newItem);
			}
		}

		private async void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button button && button.CommandParameter is ToDoItemDTO item)
			{
				await _viewModel.DeleteDataAsync(item);
			}
		}

		private async void Search_Box_TextChanged(object sender, TextChangedEventArgs e)
		{
			string searchText = Search_Box.Text;
			await _viewModel.SeaarchAsync(searchText);
		}

		private async void Search_Box_TextChangedEdit(object sender, TextChangedEventArgs e)
		{
			string searchText = Search_BoxEdit.Text;
			await _viewModel.SeaarchAsync(searchText);
		}

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

		private async void TextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Key == Key.Enter)
			{
				if (sender is TextBox textBox && textBox.DataContext is ToDoItemDTO toDoItem)
				{
					await _viewModel.AddDataAsync(toDoItem);
				}
			}
		}
	}
}
