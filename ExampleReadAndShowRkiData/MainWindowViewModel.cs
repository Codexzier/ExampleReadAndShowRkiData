using ExampleReadAndShowRkiData.Rki;
using System.Collections.Generic;
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
        private RkiCovidApiDistrictItem _districtData;
        private string _lastUpdate;
        private ICommand _searchDistrictMaxWeekIncidence;
        private ICommand _sortByWeekIncidence;
        private ICommand _loadRkiDataFromInternet;
        private ICommand _sortByDeath;
        private ObservableCollection<RkiJsonResultItem> _jsonFiles;
        private RkiJsonResultItem _selectedDateRkiJsonResult;
        private IEnumerable<RkiCovidApiDistrictItem> _countryResults;

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

        public ObservableCollection<RkiJsonResultItem> JsonFiles
        {
            get => this._jsonFiles; set
            {
                this._jsonFiles = value;
                this.OnPropertyChanged(nameof(this.JsonFiles));
            }
        }

        public RkiJsonResultItem SelectedDateRkiJsonResult
        {
            get => this._selectedDateRkiJsonResult; set
            {
                this._selectedDateRkiJsonResult = value;
                this.OnPropertyChanged(nameof(this.SelectedDateRkiJsonResult));
            }
        }

        public IEnumerable<RkiCovidApiDistrictItem> CountryResults
        {
            get => this._countryResults;
            set
            {
                this._countryResults = value;
                this.OnPropertyChanged(nameof(this.CountryResults));
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

        public RkiCovidApiDistrictItem DistrictData
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

        public ICommand LoadRkiDataFromInternet
        {
            get => this._loadRkiDataFromInternet; set
            {
                this._loadRkiDataFromInternet = value;
                this.OnPropertyChanged(nameof(this.LoadRkiDataFromInternet));
            }
        }

        public ICommand SortByDeath
        {
            get => this._sortByDeath; set
            {
                this._sortByDeath = value;
                this.OnPropertyChanged(nameof(this.SortByDeath));
            }
        }


        public RkiCovidApiDistricts RawResultDistricts { get; internal set; }

        private void OnPropertyChanged(string propertyname) => this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}