using System;
using System.Linq;
using System.Windows.Input;

namespace ExampleReadAndShowRkiData
{
    internal class CommandSortByDeath : ICommand
    {
        private MainWindowViewModel _viewModel;

        public CommandSortByDeath(MainWindowViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var sorted = this._viewModel.RawResultDistricts.districts.OrderByDescending(o => o.deaths).Select(s => new DistrictItem(s));

            this._viewModel.Districts = new System.Collections.ObjectModel.ObservableCollection<DistrictItem>(sorted.ToList());
        }
    }
}