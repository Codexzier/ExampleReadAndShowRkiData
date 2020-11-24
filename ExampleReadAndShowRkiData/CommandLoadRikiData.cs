using ExampleReadAndShowRkiData.Rki;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ExampleReadAndShowRkiData
{
    internal class CommandLoadRikiData : ICommand
    {
        private MainWindowViewModel _viewModel;
        private readonly bool _updateDataFromInternet;

        public CommandLoadRikiData(MainWindowViewModel viewModel, bool updateDataFromInternet = false)
        {
            this._viewModel = viewModel;
            this._updateDataFromInternet = updateDataFromInternet;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var result = new RkiCovidApiComponent().LoadAktualData(this._updateDataFromInternet);

            this._viewModel.LastUpdate = result.lastUpdate;
            this._viewModel.RawResultDistricts = result;
            this._viewModel.Districts = new ObservableCollection<DistrictItem>(result.districts.Select(s => new DistrictItem(s)));
        }
    }
}