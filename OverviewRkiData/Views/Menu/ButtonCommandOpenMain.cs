using OverviewRkiData.Commands;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.Main;
using System;
using System.Windows.Input;

namespace OverviewRkiData.Views.Menu
{
    internal class ButtonCommandOpenMain : ICommand
    {
        private MenuViewModel _viewModel;

        public ButtonCommandOpenMain(MenuViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            if (EventbusManager.IsViewOpen(typeof(MainView), 0))
            {
                return;
            }

            EventbusManager.OpenView<MainView>(0);
            this._viewModel.ViewOpened = EventbusManager.GetViewOpened(0);
            EventbusManager.Send<MainView, BaseMessage>(new BaseMessage(""), 0);
        }
    }
}