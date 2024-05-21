namespace WPFDemoApp
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private readonly MainViewModel<ToDoItem> _viewModel;

		private IServiceProvider _serviceProvider;
		public MainWindow(MainViewModel<ToDoItem> viewModel)
		{
			InitializeComponent();
			//_viewModel = viewModel;
			//DataContext = _viewModel;
		}
	}
}