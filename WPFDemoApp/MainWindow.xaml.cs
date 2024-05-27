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
			await _viewModel.AddData(InputTextBox.Text);
		
		}

		private async void EditButton_Click(object sender, RoutedEventArgs e)
		{
			//To Do

		}

		private async void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			//To Do

		}

		private async void DeleteButton_Click(object sender, RoutedEventArgs e)
		{
			//To Do

		}

	}
}