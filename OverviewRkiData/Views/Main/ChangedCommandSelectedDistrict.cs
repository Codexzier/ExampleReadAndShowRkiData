using OverviewRkiData.Commands;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.County;
using System;
using System.Windows.Input;

namespace OverviewRkiData.Views.Main
{
    internal class ChangedCommandSelectedDistrict : ICommand
    {
        private MainViewModel _viewModel;

        public ChangedCommandSelectedDistrict(MainViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => throw new NotImplementedException();
        public void Execute(object parameter)
        {
            if(parameter == null)
            {
                return;
            }

            if (!EventbusManager.IsViewOpen(typeof(CountyView), 1))
            {
                EventbusManager.OpenView<CountyView>(1);
                
            }
            
            EventbusManager.Send<CountyView, BaseMessage>(new BaseMessage(parameter), 1);
        }
    }
}