using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFDemoApp.Commands
{
	public class TextBoxKeyDownCommand : ICommand
	{
		private readonly MainViewModel _viewModel;

		public TextBoxKeyDownCommand(MainViewModel viewModel)
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
			if (!(parameter is KeyEventArgs e)) return;

			if (e.Key == Key.Enter)
			{
				if (e.Source is TextBox textBox && textBox.DataContext is ToDoItemDTO toDoItem)
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
	}
}
