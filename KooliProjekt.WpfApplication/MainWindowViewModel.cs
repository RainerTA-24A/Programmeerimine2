using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KooliProjekt.WpfApplication
{
    public class MainWindowViewModel : NotifyPropertyChangedBase
    {
        private readonly IApiClient _apiClient;
        private readonly ObservableCollection<Toode> _data;
        private Toode _selectedItem;

        public MainWindowViewModel() : this(new ApiClient())
        {
            
        }

        public MainWindowViewModel(IApiClient apiClient)
        {
            _data = new ObservableCollection<Toode>();
            _apiClient = apiClient;
        }

        public async Task LoadDataAsync()
        {
            var data = await _apiClient.ListTooted(1, 100);
            
            _data.Clear();
            if (data?.Value?.Results != null)
            {
                foreach (var item in data.Value.Results)
                {
                    _data.Add(item);
                }
            }
        }

        public ObservableCollection<Toode> Data
        {
            get
            {
                return _data;
            }
        }

        public Toode SelectedItem 
        { 
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                NotifyPropertyChanged();
            }
        }
    }
}
