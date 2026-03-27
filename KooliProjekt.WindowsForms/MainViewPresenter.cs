using System.Threading.Tasks;
using KooliProjekt.WindowsForms.Api;

namespace KooliProjekt.WindowsForms
{
    public class MainViewPresenter
    {
        private readonly IApiClient _apiClient;
        private readonly IMainView _mainView;

        private Toode? _selectedList;

        public MainViewPresenter(IApiClient apiClient, IMainView mainView)
        {
            _apiClient = apiClient;
            _mainView = mainView;
            _mainView.SetPresenter(this);
        }

        public async Task LoadData()
        {
            var response = await _apiClient.ListTooted(1, 100);
            if (response.HasErrors)
            {
                _mainView.ShowError("Viga andmete laadimisel", response);
                _mainView.DataSource = null;
                return;
            }

            _mainView.DataSource = response?.Value?.Results;
        }

        public void SetSelection(Toode selectedList)
        {
            _selectedList = selectedList;
            if (_selectedList == null)
            {
                _mainView.CurrentId = 0;
                _mainView.CurrentTitle = "";
                _mainView.CurrentFotoUrl = "";
                _mainView.CurrentPrice = 0;
                _mainView.CurrentStockQuantity = 0;
            }
            else
            {
                _mainView.CurrentId = _selectedList.Id;
                _mainView.CurrentTitle = _selectedList.Name;
                _mainView.CurrentFotoUrl = _selectedList.FotoURL ?? "";
                _mainView.CurrentPrice = _selectedList.Price;
                _mainView.CurrentStockQuantity = _selectedList.StockQuantity;
            }
        }

        public async Task Save()
        {
            var toode = new Toode
            {
                Id = _mainView.CurrentId,
                Name = _mainView.CurrentTitle,
                FotoURL = _mainView.CurrentFotoUrl,
                Price = _mainView.CurrentPrice,
                StockQuantity = _mainView.CurrentStockQuantity
            };

            var result = await _apiClient.SaveToode(toode);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga salvestamisel", result);
                return;
            }

            await LoadData();
        }

        public async Task Delete()
        {
            if(!_mainView.ConfirmDelete())
            {
                return;
            }

            var result = await _apiClient.DeleteToode(_mainView.CurrentId);
            if (result.HasErrors)
            {
                _mainView.ShowError("Viga kustutamisel", result);
                return;
            }

            await LoadData();
        }
    }
}