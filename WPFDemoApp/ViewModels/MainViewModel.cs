namespace WPFDemoApp.ViewModels
{
	// MainViewModel class implementing INotifyPropertyChanged to notify the UI about property changes
	public class MainViewModel<TEntity> : INotifyPropertyChanged where TEntity : class, IEntityId, IEntityHasBeenDeleted,IEntityTextContent
	{
		
		private readonly IGetAllDataUseCase<TEntity> _getAllDataUseCase;

		// Private field to store the collection of entities
		private ObservableCollection<TEntity> _entities;

		// Public property to expose the collection of entities to the UI
		// The set accessor updates the _entities field and raises the OnPropertyChanged method to notify the UI.
		public ObservableCollection<TEntity> Entities
		{
			get => _entities;
			set
			{
				_entities = value;
				OnPropertyChanged();
			}
		}

		// Command property to load data when invoked
		public ICommand LoadDataCommand { get; }

		public MainViewModel(IGetAllDataUseCase<TEntity> getAllDataUseCase)
		{
			_getAllDataUseCase = getAllDataUseCase;
			LoadDataCommand = new RelayCommand(async _ => await LoadDataAsync());
		}

		// Asynchronous method to load data using the use case and update the Entities property
		private async Task LoadDataAsync()
		{
			var data = await _getAllDataUseCase.ExecuteAsync();
			Entities = new ObservableCollection<TEntity>(data);
		}

		// Event to notify the UI about property changes
		public event PropertyChangedEventHandler PropertyChanged;

		// Method to raise the PropertyChanged event, notifying the UI about property changes
		// The [CallerMemberName] attribute allows the caller's name to be used as the argument if no explicit argument is provided.
		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
