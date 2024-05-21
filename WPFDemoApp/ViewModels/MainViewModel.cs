using System.Collections.ObjectModel;

namespace WPFDemoApp.ViewModels
{
	// MainViewModel class implementing INotifyPropertyChanged to notify the UI about property changes
	public class MainViewModel<TEntity> : INotifyPropertyChanged where TEntity : class, IEntityId, IEntityHasBeenDeleted, IEntityTextContent
	{
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

		private readonly IGetAllDataUseCase<TEntity> _getAllDataUseCase;

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

		public MainViewModel(IGetAllDataUseCase<TEntity> getAllDataUseCase)
		{
			_getAllDataUseCase = getAllDataUseCase;
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
				var data = await _getAllDataUseCase.ExecuteAsync();
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
	}
}
