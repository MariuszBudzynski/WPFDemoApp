using Moq;
using WPFDemoApp.Core.Entieties;
using WPFDemoApp.Core.UseCases;
using WPFDemoApp.Core.UseCases.Interfaces;
using WPFDemoApp.DTOS;
using WPFDemoApp.ViewModels;

namespace WPFDemoApp.Tests.ModelViewTest
{
	public class ModelViewTest
	{
		private readonly Mock<IGetAllDataUseCase> _mockGetAllDataUseCase;
		private readonly Mock<ISaveSingleDataItemUseCase> _mockSaveSingleDataItemUseCase;
		private readonly Mock<IDeleteItemUseCase> _mockDeleteItemUseCase;
		private readonly Mock<IUpdateDataUseCase> _mockUpdateDataUseCase;

		public ModelViewTest()
        {
			_mockGetAllDataUseCase = new Mock<IGetAllDataUseCase>();
			_mockSaveSingleDataItemUseCase = new Mock<ISaveSingleDataItemUseCase>();
			_mockDeleteItemUseCase = new Mock<IDeleteItemUseCase>();
			_mockUpdateDataUseCase = new Mock<IUpdateDataUseCase>();

		}
		[Fact]
		public async Task LoadDataAsync_WhenFilterIsAll_ShouldLoadAllItems()
		{
			//Arrange
			var toDoItems = new List<ToDoItem>
			{
				new ToDoItem { Id = 1, TextContent = "Task 1", HasBeenCompleted = false, DataID = Guid.NewGuid() },
				new ToDoItem { Id = 2, TextContent = "Task 2", HasBeenCompleted = true, DataID = Guid.NewGuid() }
			};

			_mockGetAllDataUseCase.Setup(x=> x.ExecuteAsync<ToDoItem>()).ReturnsAsync(toDoItems);
			var viewModel = new MainViewModel(_mockGetAllDataUseCase.Object, _mockSaveSingleDataItemUseCase.Object, _mockDeleteItemUseCase.Object, _mockUpdateDataUseCase.Object)
			{
				Filter = "All"
			};

			// Act
			await viewModel.RefreshDataAsync();

			// Assert
			Assert.NotNull(viewModel.ToDoItemDTOList);
			Assert.Equal(2, viewModel.ToDoItemDTOList.Count);
		}

		[Fact]
		public async Task LoadDataAsync_WhenFilterIsCompleted_ShouldLoadCompletedItems()
		{
			// Arrange
			var toDoItems = new List<ToDoItem>
			{
				new ToDoItem { Id = 1, TextContent = "Task 1", HasBeenCompleted = false, DataID = Guid.NewGuid() },
				new ToDoItem { Id = 2, TextContent = "Task 2", HasBeenCompleted = true, DataID = Guid.NewGuid() }
			};

			_mockGetAllDataUseCase.Setup(x => x.ExecuteAsync<ToDoItem>()).ReturnsAsync(toDoItems);
			var viewModel = new MainViewModel(_mockGetAllDataUseCase.Object, _mockSaveSingleDataItemUseCase.Object, _mockDeleteItemUseCase.Object, _mockUpdateDataUseCase.Object)
			{
				Filter = "Completed"
			};

			// Act
			await viewModel.RefreshDataAsync();

			// Assert
			Assert.NotNull(viewModel.ToDoItemDTOList);
			Assert.Single(viewModel.ToDoItemDTOList);
			Assert.True(viewModel.ToDoItemDTOList.First().HasBeenCompleted);
		}

		[Fact]
		public async Task LoadDataAsync_WhenFilterIsNotCompleted_ShouldLoadNotCompletedItems()
		{
			// Arrange
			var toDoItems = new List<ToDoItem>
			{
				new ToDoItem { Id = 1, TextContent = "Task 1", HasBeenCompleted = false, DataID = Guid.NewGuid() },
				new ToDoItem { Id = 2, TextContent = "Task 2", HasBeenCompleted = true, DataID = Guid.NewGuid() }
			};

			_mockGetAllDataUseCase.Setup(x => x.ExecuteAsync<ToDoItem>()).ReturnsAsync(toDoItems);
			var viewModel = new MainViewModel(_mockGetAllDataUseCase.Object, _mockSaveSingleDataItemUseCase.Object, _mockDeleteItemUseCase.Object, _mockUpdateDataUseCase.Object)
			{
				Filter = "NotCompleted"
			};

			// Act
			await viewModel.RefreshDataAsync();

			// Assert
			Assert.NotNull(viewModel.ToDoItemDTOList);
			Assert.Single(viewModel.ToDoItemDTOList);
			Assert.False(viewModel.ToDoItemDTOList.First().HasBeenCompleted);
		}

		[Fact]
		public async Task DeleteDataAsync_ShouldDeleteData()
		{
			// Arrange
			var toDoItems = new List<ToDoItem>
			{
				new ToDoItem { Id = 1, TextContent = "Task 1", HasBeenCompleted = false, DataID = Guid.NewGuid() },
				new ToDoItem { Id = 2, TextContent = "Task 2", HasBeenCompleted = true, DataID = Guid.NewGuid() }
			};

			var itemToDelete = toDoItems.First();

			_mockGetAllDataUseCase.Setup(x => x.ExecuteAsync<ToDoItem>()).ReturnsAsync(toDoItems);
			_mockDeleteItemUseCase.Setup(x => x.ExecuteAsync<ToDoItem>(It.IsAny<ToDoItem>())).Returns(Task.CompletedTask);
		    var viewModel = new MainViewModel(_mockGetAllDataUseCase.Object, _mockSaveSingleDataItemUseCase.Object, _mockDeleteItemUseCase.Object, _mockUpdateDataUseCase.Object);

			// Act
			await viewModel.RefreshDataAsync();
			await viewModel.DeleteDataAsync(new ToDoItemDTO(itemToDelete.DataID, itemToDelete.TextContent, itemToDelete.HasBeenCompleted));

			// Assert
			_mockDeleteItemUseCase.Verify(x => x.ExecuteAsync(It.Is<ToDoItem>(y =>
			   y.DataID == itemToDelete.DataID &&
			   y.TextContent == itemToDelete.TextContent &&
			   y.HasBeenCompleted == itemToDelete.HasBeenCompleted)), Times.Once);
		}

		[Fact]
		public async Task AddDataAsync_ShouldAddData()
		{
			// Arrange
			var toDoItems = new List<ToDoItem>
			{
				new ToDoItem { Id = 1, TextContent = "Task 1", HasBeenCompleted = false, DataID = Guid.NewGuid() },
				new ToDoItem { Id = 2, TextContent = "Task 2", HasBeenCompleted = true, DataID = Guid.NewGuid() }
			};

			_mockGetAllDataUseCase.Setup(x => x.ExecuteAsync<ToDoItem>()).ReturnsAsync(toDoItems);
			_mockSaveSingleDataItemUseCase.Setup(x => x.ExecuteAsync<ToDoItem>(It.IsAny<ToDoItem>())).Returns(Task.CompletedTask);
			var viewModel = new MainViewModel(_mockGetAllDataUseCase.Object, _mockSaveSingleDataItemUseCase.Object, _mockDeleteItemUseCase.Object, _mockUpdateDataUseCase.Object);

			//Act
			await viewModel.RefreshDataAsync();
			var initialCount = toDoItems.Count;
			var newToDoItemDTO = new ToDoItemDTO(Guid.NewGuid(), "New Task", false);
			var newToDoItem = new ToDoItem() {DataID = newToDoItemDTO.DataId , TextContent = newToDoItemDTO.TextContent,HasBeenCompleted=newToDoItemDTO.HasBeenCompleted };
			await viewModel.AddDataAsync(newToDoItemDTO);
			toDoItems.Add(newToDoItem);
			var finalCount = toDoItems.Count;

			// Assert
			_mockSaveSingleDataItemUseCase.Verify(x => x.ExecuteAsync(It.IsAny<ToDoItem>()), Times.Once);
			Assert.Equal(initialCount + 1, finalCount);
		}
	}
}
