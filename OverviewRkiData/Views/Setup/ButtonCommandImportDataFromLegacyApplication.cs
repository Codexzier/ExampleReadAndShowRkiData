using OverviewRkiData.Commands;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Controls.FolderBrowser;
using OverviewRkiData.Views.Dialog;
using System;
using System.Windows.Input;

namespace OverviewRkiData.Views.Setup
{
    internal class ButtonCommandImportDataFromLegacyApplication : ICommand
    {
        private SetupViewModel _viewModel;

        public ButtonCommandImportDataFromLegacyApplication(SetupViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            if (EventbusManager.IsViewOpen<DialogView>(10))
            {
                return;
            }

            EventbusManager.OpenView<DialogView>(10);

            var ddc = new DataDialogContent();
            ddc.Header = "Folder Browser";
            EventbusManager.Send<DialogView, BaseMessage>(new BaseMessage(ddc), 10);
        }
    }
}