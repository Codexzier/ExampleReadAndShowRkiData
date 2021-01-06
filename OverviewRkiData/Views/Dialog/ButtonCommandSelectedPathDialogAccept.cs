﻿using System;
using System.Windows.Input;

namespace OverviewRkiData.Views.Dialog
{
    internal class ButtonCommandSelectedPathDialogAccept : ICommand
    {
        private readonly DialogViewModel _viewModel;

        public ButtonCommandSelectedPathDialogAccept(DialogViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            // TODO: setup the importer
            var selectedFolder = this._viewModel.SelectedDirectoryPath.FolderName;
        }
    }
}