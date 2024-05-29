﻿namespace WPFDemoApp.ViewModels
{
	// MainViewModel class implementing INotifyPropertyChanged to notify the UI about property changes
	public class MainViewModel : INotifyPropertyChanged
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IGetAllDataUseCase _getAllDataUseCase;
		private readonly ISaveSingleDataItemUseCase _saveSingleDataItemUseCase;
		private readonly ISoftDeleteItemUseCase _softDeleteItemUseCase;
		private readonly IUpdateDataUseCase _updateDataUseCase;


		private ObservableCollection<ToDoItemDTO> _textContentList;

		public ObservableCollection<ToDoItemDTO> TextContentList
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
				var textContentList = new ObservableCollection<ToDoItemDTO>(dtoData);



				TextContentList = textContentList;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while loading data.");
			}
		}


		public async Task DeleteDataAsync(ToDoItemDTO data)
		{
			var item = CreateNewToDoItem(data.TextContent,data.HasBeenCompleted);
			await _softDeleteItemUseCase.ExecuteAsync(item);
			await RefreshDataAsync();
		}

		private async Task RefreshDataAsync()
		{
			await LoadDataAsyn();
		}

		public async Task AddDataAsync(ToDoItemDTO data)
		{
			if (data.TextContent.IsNullOrEmpty())
			{
				MessageBox.Show("Please enter an item before adding.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				return;
			}
			try
			{
				var dataToSave = CreateNewToDoItem(data.TextContent);
				await _saveSingleDataItemUseCase.ExecuteAsync(dataToSave);
				await RefreshDataAsync();
			}
			catch (Exception ex)
			{

				MessageBox.Show($"An error occurred while saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}


		public async Task SeaarchAsync(string searchPhrase)
		{
			try
			{
				var data = await _getAllDataUseCase.ExecuteAsync<ToDoItem>();
				var dtoData = data.ToDto();
				var filteredData = dtoData.Where(x => x.TextContent.Contains(searchPhrase.ToLower()));
				var textContentList = new ObservableCollection<ToDoItemDTO>(filteredData);



				TextContentList = textContentList;
			}
			catch (Exception ex)
			{
				_logger.Error(ex, "An error occurred while filtering data.");
			}

		}

		public async Task UpdateCheckbox(ToDoItemDTO item)
		{
			var itemTodo = CreateNewToDoItem(item.TextContent,item.HasBeenCompleted);
			await _updateDataUseCase.ExecuteAsync(itemTodo);
		}

		private ToDoItem CreateNewToDoItem(string textContext, bool completed = false)
		{
			return new ToDoItem
			{
				
				TextContent = textContext,
				HasBeenCompleted = completed
			};
		}
	}
}
