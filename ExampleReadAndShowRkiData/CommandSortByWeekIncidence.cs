using System;
using System.Windows.Input;
using System.Linq;

namespace ExampleReadAndShowRkiData
{
    internal class CommandSortByWeekIncidence : ICommand
    {
        private MainWindowViewModel _viewModel;

        public CommandSortByWeekIncidence(MainWindowViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var sorted = this._viewModel.RawResultDistricts.districts.OrderByDescending(o => o.weekIncidence).Select(s => new DistrictItem(s));

            this._viewModel.Districts = new System.Collections.ObjectModel.ObservableCollection<DistrictItem>(sorted.ToList());
        }
    }
}