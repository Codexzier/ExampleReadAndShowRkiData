using System;
using System.Linq;
using System.Windows.Input;

namespace ExampleReadAndShowRkiData
{
    internal class CommandSearchDistrictMaxWeekIncidence : ICommand
    {
        private MainWindowViewModel _viewModel;

        public CommandSearchDistrictMaxWeekIncidence(MainWindowViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var maxValue = this._viewModel.RawResultDistricts.districts.Max(w => w.weekIncidence);
            var resultDistrict = this._viewModel.RawResultDistricts.districts.First(f => f.weekIncidence >= maxValue);

            this._viewModel.SearchContains = resultDistrict.name;
            this._viewModel.SelectedDistrict = new DistrictItem(resultDistrict);
        }
    }
}