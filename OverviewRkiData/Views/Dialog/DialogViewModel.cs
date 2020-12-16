using OverviewRkiData.Views.Base;
using System.Windows.Input;

namespace OverviewRkiData.Views.Dialog
{
    internal class DialogViewModel : BaseViewModel
    {
        private string _header;
        private ICommand _buttonClickCloseDialogView = new DoCloseDialogView();

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
    }
}