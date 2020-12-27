using OverviewRkiData.Views.Base;
using System.Windows.Input;

namespace OverviewRkiData.Views.Dialog
{
    internal class DialogViewModel : BaseViewModel
    {
        private string _header;
        private ICommand _buttonClickCloseDialogView = new DoCloseDialogView();
        private ICommand _commandSelectedPathDialogAccept;
        private string _selectedDirectoryPath = string.Empty;

        public string Header
        {
            get => this._header; set
            {
                this._header = value;
                this.OnNotifyPropertyChanged(nameof(this.Header));
            }
        }

        public ICommand ButtonClickCloseDialogView
        {
            get => this._buttonClickCloseDialogView; set
            {
                this._buttonClickCloseDialogView = value;
                this.OnNotifyPropertyChanged(nameof(this.ButtonClickCloseDialogView));
            }
        }

        public ICommand CommandSelectedPathDialogAccept
        {
            get => this._commandSelectedPathDialogAccept;
            set
            {
                this._commandSelectedPathDialogAccept = value;
                this.OnNotifyPropertyChanged(nameof(this.CommandSelectedPathDialogAccept));
            }
        }

        public string SelectedDirectoryPath
        {
            get => this._selectedDirectoryPath;
            set
            {
                this._selectedDirectoryPath = value;
                this.OnNotifyPropertyChanged(nameof(this.SelectedDirectoryPath));
            }
        }
    }
}