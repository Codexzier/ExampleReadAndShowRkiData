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
        private string _searchContains;
        private DistrictItem _selectedDistrict;
        private RkiCovidApiDistrictItem _districtData;
        private string _lastUpdate;
        private ICommand _sortByWeekIncidence;
        private ICommand _sortByDeath;
        private ObservableCollection<RkiJsonResultItem> _jsonFiles;
        private RkiJsonResultItem _selectedDateRkiJsonResult;
        private IEnumerable<RkiCovidApiDistrictItem> _countyResults;
        private ICommand _loadData;
        private IEnumerable<RkiCovidApiDistrictItem> _picketResults;
        private ICommand _listBoxSelectionChanged;
        private ICommand _selectAllCountyWihtLast3DaysUps;

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

        public IEnumerable<RkiCovidApiDistrictItem> CountyResults
        {
            get => this._countyResults;
            set
            {
                this._countyResults = value;
                this.OnPropertyChanged(nameof(this.CountyResults));
            }
        }

        public IEnumerable<RkiCovidApiDistrictItem> PicketResults
        {
            get => this._picketResults;
            set
            {
                this._picketResults = value;
                this.OnPropertyChanged(nameof(this.PicketResults));
            }
        }

        public ICommand LoadData
        {
            get => this._loadData;
            set
            {
                this._loadData = value;
                this.OnPropertyChanged(nameof(this.LoadData));
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

        public ICommand SortByWeekIncidence
        {
            get => this._sortByWeekIncidence; set
            {
                this._sortByWeekIncidence = value;
                this.OnPropertyChanged(nameof(this.SortByWeekIncidence));
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

        public ICommand ListBoxSelectionChanged
        {
            get => this._listBoxSelectionChanged;
            set
            {
                this._listBoxSelectionChanged = value;
                this.OnPropertyChanged(nameof(this.ListBoxSelectionChanged));
            }
        }

        public ICommand SelectAllCountyWihtLast3DaysUps
        {
            get => this._selectAllCountyWihtLast3DaysUps;
            set
            {
                this._selectAllCountyWihtLast3DaysUps = value;
                this.OnPropertyChanged(nameof(this.SelectAllCountyWihtLast3DaysUps));
            }
        }


        public RkiCovidApiDistricts RawResultDistricts { get; internal set; }

        private void OnPropertyChanged(string propertyname) => this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyname));

        public event PropertyChangedEventHandler PropertyChanged;
    }
}