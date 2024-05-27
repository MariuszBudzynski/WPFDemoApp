namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewModel _viewModel;
		private readonly ISaveSingleDataItemUseCase _saveSingleDataItemUseCase;

		public MainWindow(MainViewModel viewModel, ISaveSingleDataItemUseCase saveSingleDataItemUseCase)
		{
			InitializeComponent();
			_viewModel = viewModel;
			DataContext = _viewModel;
			_saveSingleDataItemUseCase = saveSingleDataItemUseCase;
		}

		private async void AddButton_Click(object sender, RoutedEventArgs e)
		{
			try
			{
				var dataToSave = CreateNewToDoItem(InputTextBox.Text);
				await _saveSingleDataItemUseCase.ExecuteAsync(dataToSave);
			}
			catch (Exception ex)
			{

				MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
        }

		private ToDoItem CreateNewToDoItem(string textContext)
		{
			return new ToDoItem
			{
				HasBeenDeleted = false,
				TextContent = textContext
			};
		}

	}
}