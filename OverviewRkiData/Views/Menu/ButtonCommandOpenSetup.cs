using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.Setup;
using System;
using System.Windows.Input;

namespace OverviewRkiData.Views.Menu
{
    internal class ButtonCommandOpenSetup : ICommand
    {
        private MenuViewModel _viewModel;

        public ButtonCommandOpenSetup(MenuViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (EventbusManager.IsViewOpen(typeof(SetupView), 0))
            {
                return;
            }

            EventbusManager.OpenView<SetupView>(0);
            this._viewModel.ViewOpened = EventbusManager.GetViewOpened(0);
        }
    }
}