using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KooliProjekt.WpfApplication
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly ApiClient _apiClient;
        private IList<Toode> _data;
        private Toode _selectedItem;

        public MainWindowViewModel()
        {
            _apiClient = new ApiClient();
            LoadData();
        }

        public IList<Toode> Data 
        { 
            get => _data;
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        public Toode SelectedItem 
        { 
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        private async void LoadData()
        {
            var result = await _apiClient.ListTooted(1, 100);
            if (result.Value != null)
            {
                Data = result.Value.Results;
            }
            else
            {
                // Fallback to mock data if API fails
                Data = new List<Toode>
                {
                    new Toode { Id = 1, Name = "Test Toode 1", Price = 10, StockQuantity = 5 },
                    new Toode { Id = 2, Name = "Test Toode 2", Price = 20, StockQuantity = 10 }
                };
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
