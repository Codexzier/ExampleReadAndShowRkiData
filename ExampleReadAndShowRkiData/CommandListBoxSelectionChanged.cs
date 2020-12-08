using System;
using System.Windows.Input;

namespace ExampleReadAndShowRkiData
{
    internal class CommandListBoxSelectionChanged : ICommand
    {
        private MainWindowViewModel _viewModel;

        public CommandListBoxSelectionChanged(MainWindowViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

        }
    }
}