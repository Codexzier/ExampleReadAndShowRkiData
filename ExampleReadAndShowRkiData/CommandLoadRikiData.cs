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

        public CommandLoadRikiData(MainWindowViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var result = new RkiCovidApiComponent().LoadAktualData();

            this._viewModel.LastUpdate = result.lastUpdate;
            this._viewModel.RawResultDistricts = result;
            this._viewModel.Districts = new ObservableCollection<DistrictItem>(result.districts.Select(s => new DistrictItem(s)));
        }
    }
}