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
            }
            else
            {
                _mainView.CurrentId = _selectedList.Id;
                _mainView.CurrentTitle = _selectedList.Name;
            }
        }

        public async Task Save()
        {
            var toode = new Toode
            {
                Id = _mainView.CurrentId,
                Name = _mainView.CurrentTitle,
                Price = _selectedList?.Price ?? 10,
                StockQuantity = _selectedList?.StockQuantity ?? 10
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