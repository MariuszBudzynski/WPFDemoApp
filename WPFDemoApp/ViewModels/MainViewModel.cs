namespace WPFDemoApp.ViewModels
{
	// MainViewModel class implementing INotifyPropertyChanged to notify the UI about property changes
	public class MainViewModel : INotifyPropertyChanged
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IGetAllDataUseCase _getAllDataUseCase;
		private readonly ISaveSingleDataItemUseCase _saveSingleDataItemUseCase;
		private readonly ISoftDeleteItemUseCase _softDeleteItemUseCase;
		private readonly IUpdateDataUseCase _updateDataUseCase;


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


		private bool _hasBeenCompleted;

		public bool HasBeenCompleted
		{
			get => _hasBeenCompleted;
			set
			{
				_hasBeenCompleted = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public MainViewModel(	IGetAllDataUseCase getAllDataUseCase,
								ISaveSingleDataItemUseCase saveSingleDataItemUseCase,
								ISoftDeleteItemUseCase softDeleteItemUseCase,
								IUpdateDataUseCase updateDataUseCase)
		{
			_getAllDataUseCase = getAllDataUseCase;
			_saveSingleDataItemUseCase = saveSingleDataItemUseCase;
			_softDeleteItemUseCase = softDeleteItemUseCase;
			_updateDataUseCase = updateDataUseCase;
			LoadDataAsyn(); // Automatic data loading when creating a ViewModel
		}

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		// Asynchronous method to load data using the use case and update the Entities property
		private async Task LoadDataAsyn()
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

		public async Task DeleteDataAsync(string data)
		{
			var item = CreateNewToDoItem(data);
			await _softDeleteItemUseCase.ExecuteAsync(item);
			await RefreshDataAsync();
		}

		private async Task RefreshDataAsync()
		{
			await LoadDataAsyn();
		}

		public async Task AddDataAsync(string inputTextData)
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
				TextContent = textContext,
				HasBeenCompleted = false
			};
		}

		public async Task SeaarchAsync(string searchPhrase)
		{
			try
			{
				var data = await _getAllDataUseCase.ExecuteAsync<ToDoItem>();
				var dtoData = data.ToDto();
				var filteredData = dtoData.Where(x => x.TextContent.Contains(searchPhrase.ToLower()));
				var textContentList = new ObservableCollection<string>();

				foreach (var item in filteredData)
				{
					textContentList.Add(item.TextContent);
				}

				TextContentList = textContentList;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while filtering data.");
			}

		}

		public async Task UpdateCheckbox(string name,bool state)
		{
			var data = (await _getAllDataUseCase.ExecuteAsync<ToDoItem>()).FirstOrDefault(x=>x.TextContent==name);
			data.HasBeenCompleted = state;
			await _updateDataUseCase.ExecuteAsync(data);
			//await RefreshDataAsync();

		}

	}
}
