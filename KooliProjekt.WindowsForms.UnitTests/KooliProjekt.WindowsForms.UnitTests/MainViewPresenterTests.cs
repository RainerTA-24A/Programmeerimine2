using KooliProjekt.WindowsForms.Api;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.WindowsForms.UnitTests
{
    public class MainViewPresenterTests
    {
        private readonly Mock<IApiClient> _apiClientMock;
        private readonly Mock<IMainView> _mainViewMock;
        private readonly MainViewPresenter _presenter;

        public MainViewPresenterTests()
        {
            _apiClientMock = new Mock<IApiClient>();
            _mainViewMock = new Mock<IMainView>();
            _presenter = new MainViewPresenter(_apiClientMock.Object, _mainViewMock.Object);
        }

        [Fact]
        public async Task LoadData_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult<PagedResult<Toode>>();
            faultyResponse.AddError("An error occurred while fetching data.");

            _apiClientMock
                .Setup(client => client.ListTooted(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = null)
                .Verifiable();

            // Act
            await _presenter.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task LoadData_should_set_DataSource_with_valid_response()
        {
            // Arrange
            var validResponse = new OperationResult<PagedResult<Toode>>
            {
                Value = new PagedResult<Toode>
                {
                    Results = new List<Toode>
                    {
                        new Toode { Id = 1, Name = "Test Toode 1" },
                        new Toode { Id = 2, Name = "Test Toode 2" }
                    }
                }
            };

            _apiClientMock
                .Setup(client => client.ListTooted(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(validResponse)
                .Verifiable();
            _mainViewMock
                .SetupSet(view => view.DataSource = validResponse.Value.Results)
                .Verifiable();

            // Act
            await _presenter.LoadData();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public void SetSelection_should_clear_fields_with_null_selection()
        {
            // Arrange
            var selectedList = (Toode?)null;

            _mainViewMock.SetupSet(view => view.CurrentId = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentTitle = "").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentFotoUrl = "").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentPrice = 0).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentStockQuantity = 0).Verifiable();

            // Act
            _presenter.SetSelection(selectedList);

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public void SetSelection_should_set_fields_with_valid_selection()
        {
            // Arrange
            var selectedList = new Toode { Id = 1, Name = "Toode", FotoURL = "url", Price = 10, StockQuantity = 5 };

            _mainViewMock.SetupSet(view => view.CurrentId = 1).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentTitle = "Toode").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentFotoUrl = "url").Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentPrice = 10).Verifiable();
            _mainViewMock.SetupSet(view => view.CurrentStockQuantity = 5).Verifiable();

            // Act
            _presenter.SetSelection(selectedList);

            // Assert
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult();
            faultyResponse.AddError("An error occurred while saving data.");

            _apiClientMock
                .Setup(client => client.SaveToode(It.IsAny<Toode>()))
                .ReturnsAsync(faultyResponse)
                .Verifiable();
            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();

            // Act
            await _presenter.Save();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_call_LoadData_with_valid_response()
        {
            // Arrange
            var validResponse = new OperationResult();

            _apiClientMock
                .Setup(client => client.SaveToode(It.IsAny<Toode>()))
                .ReturnsAsync(validResponse)
                .Verifiable();

            // setting up mock for loading data
            var loadResponse = new OperationResult<PagedResult<Toode>>();
            _apiClientMock
                 .Setup(client => client.ListTooted(It.IsAny<int>(), It.IsAny<int>()))
                 .ReturnsAsync(loadResponse)
                 .Verifiable();

            // Act
            await _presenter.Save();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_return_when_user_didnot_confirmed()
        {
            // Arrange
            _mainViewMock.Setup(view => view.ConfirmDelete()).Returns(false).Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _mainViewMock.VerifyAll();
            _apiClientMock.Verify(c => c.DeleteToode(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task Delete_should_call_ShowError_with_faulty_response()
        {
            // Arrange
            var faultyResponse = new OperationResult();
            faultyResponse.AddError("Error");
            _mainViewMock.Setup(view => view.ConfirmDelete()).Returns(true).Verifiable();
            _mainViewMock.SetupGet(view => view.CurrentId).Returns(1).Verifiable();

            _apiClientMock
                .Setup(client => client.DeleteToode(1))
                .ReturnsAsync(faultyResponse)
                .Verifiable();

            _mainViewMock
                .Setup(view => view.ShowError(It.IsAny<string>(), It.IsAny<OperationResult>()))
                .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_call_LoadData_with_valid_response()
        {
            // Arrange
            var validResponse = new OperationResult();

            _mainViewMock.Setup(view => view.ConfirmDelete()).Returns(true).Verifiable();
            _mainViewMock.SetupGet(view => view.CurrentId).Returns(1).Verifiable();

            _apiClientMock
                .Setup(client => client.DeleteToode(1))
                .ReturnsAsync(validResponse)
                .Verifiable();

            // setting up mock for loading data
            var loadResponse = new OperationResult<PagedResult<Toode>>();
            _apiClientMock
                 .Setup(client => client.ListTooted(It.IsAny<int>(), It.IsAny<int>()))
                 .ReturnsAsync(loadResponse)
                 .Verifiable();

            // Act
            await _presenter.Delete();

            // Assert
            _apiClientMock.VerifyAll();
            _mainViewMock.VerifyAll();
        }
    }
}
