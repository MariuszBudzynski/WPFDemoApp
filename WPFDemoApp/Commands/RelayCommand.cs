//namespace WPFDemoApp.Commands
//{
//	// RelayCommand class implements ICommand interface to create custom commands
//	public class RelayCommand : ICommand
//	{
//		// Private fields to store the execute and canExecute delegates
//		private readonly Action<object> _execute;
//		private readonly Func<object, bool> _canExecute;

//		// Constructor to initialize the command with the execute action and an optional canExecute function
//		public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
//		{
//			_execute = execute ?? throw new ArgumentNullException(nameof(execute));
//			_canExecute = canExecute;
//		}

//		// Method to determine if the command can execute
//		// If canExecute is not provided, it defaults to true
//		public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

//		// Method to execute the command's action
//		public void Execute(object parameter) => _execute(parameter);

//		// Event to handle changes in the command's ability to execute
//		public event EventHandler CanExecuteChanged
//		{
//			// Adds a handler for CanExecuteChanged event to the CommandManager's RequerySuggested event
//			add => CommandManager.RequerySuggested += value;
//			// Removes the handler from the CommandManager's RequerySuggested event
//			remove => CommandManager.RequerySuggested -= value;
//		}
//	}
//}
