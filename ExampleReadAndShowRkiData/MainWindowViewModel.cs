using ExampleReadAndShowRkiData.Rki;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace ExampleReadAndShowRkiData
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DistrictItem> _districts;
        private ICommand _loadRkiData;
        private string _searchContains;
        private DistrictItem _selectedDistrict;
        private RkiCovidApiCountryItem _districtData;
        private string _lastUpdate;
        private ICommand _searchDistrictMaxWeekIncidence;
        private ICommand _sortByWeekIncidence;

        public ObservableCollection<DistrictItem> Districts
        {
            get => this._districts;
            set
            {
                this._districts = value;
                this.OnPropertyChanged(nameof(this.Districts));
            }
        }

        public DistrictItem SelectedDistrict
        {
            get => this._selectedDistrict; set
            {
                this._selectedDistrict = value;
                this.OnPropertyChanged(nameof(this.SelectedDistrict));
            }
        }

        public ICommand LoadRkiData
        {
            get => this._loadRkiData; set
            {
                this._loadRkiData = value;
                this.OnPropertyChanged(nameof(this.LoadRkiData));
            }
        }


        public string SearchContains
        {
            get => this._searchContains; set
            {
                this._searchContains = value;
                this.OnPropertyChanged(nameof(this.SearchContains));
            }
        }

        public RkiCovidApiCountryItem DistrictData
        {
            get => this._districtData; set
            {
                this._districtData = value;
                this.OnPropertyChanged(nameof(this.DistrictData));
            }
        }

        public string LastUpdate
        {
            get => this._lastUpdate; set
            {
                this._lastUpdate = value;
                this.OnPropertyChanged(nameof(this.LastUpdate));
            }
        }

        public ICommand SearchDistrictMaxWeekIncidence
        {
            get => this._searchDistrictMaxWeekIncidence; set
            {
                this._searchDistrictMaxWeekIncidence = value;
                this.OnPropertyChanged(nameof(this.SearchDistrictMaxWeekIncidence));
            }
        }

        public ICommand SortByWeekIncidence
        {
            get => this._sortByWeekIncidence; set
            {
                this._sortByWeekIncidence = value;
                this.OnPropertyChanged(nameof(this.SortByWeekIncidence));
            }
        }

        public RkiCovidDistricts RawResultDistricts { get; internal set; }

        private void OnPropertyChanged(string propertyname) => this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}