namespace WPFDemoApp.ViewModels
{
	// MainViewModel class implementing INotifyPropertyChanged to notify the UI about property changes
	public class MainViewModel : INotifyPropertyChanged
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IGetAllDataUseCase _getAllDataUseCase;
		private readonly ISaveSingleDataItemUseCase _saveSingleDataItemUseCase;


		private ObservableCollection<string> _textContentList;

		public ObservableCollection<string> TextContentList
		{
			get => _textContentList;
			set
			{
				_textContentList = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public MainViewModel(IGetAllDataUseCase getAllDataUseCase, ISaveSingleDataItemUseCase saveSingleDataItemUseCase)
		{
			_getAllDataUseCase = getAllDataUseCase;
			_saveSingleDataItemUseCase = saveSingleDataItemUseCase;
			LoadDataAsync(); // Automatic data loading when creating a ViewModel
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Asynchronous method to load data using the use case and update the Entities property
		private async Task LoadDataAsync()
		{
			try
			{
				var data = await _getAllDataUseCase.ExecuteAsync<ToDoItem>();
				var dtoData = data.ToDto();
				var textContentList = new ObservableCollection<string>();

				foreach (var item in data)
				{
					textContentList.Add(item.TextContent);
				}

				TextContentList = textContentList;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while loading data.");
			}
		}

		private async Task RefreshDataAsync()
		{
			await LoadDataAsync();
		}

		public async Task AddData(string inputTextData)
		{
			if (inputTextData.IsNullOrEmpty())
			{
				MessageBox.Show("Please enter an item before adding.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			try
			{
				var dataToSave = CreateNewToDoItem(inputTextData);
				await _saveSingleDataItemUseCase.ExecuteAsync(dataToSave);
				await RefreshDataAsync();
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
