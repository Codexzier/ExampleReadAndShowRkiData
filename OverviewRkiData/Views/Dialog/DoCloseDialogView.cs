using System;
using System.Windows.Input;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.DialogContent;

namespace OverviewRkiData.Views.Dialog
{
    internal class DoCloseDialogView : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            EventbusManager.CloseView<DialogContentView>(2);
            EventbusManager.CloseView<DialogView>(10);
        }
    }
}