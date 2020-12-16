using System;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.Base;

namespace OverviewRkiData.Views.Menu
{
    public class MenuViewModel : BaseViewModel
    {
        private ViewOpen _viewOpened = ViewOpen.Nothing;
        private bool _isServiceAvailable;

        public ViewOpen ViewOpened
        {
            get => this._viewOpened;
            set
            {
                this._viewOpened = value;
                this.OnNotifyPropertyChanged(nameof(this.ViewOpened));
            }
        }

        public bool IsServiceAvailable
        {
            get => this._isServiceAvailable; set
            {
                this._isServiceAvailable = value;
                this.OnNotifyPropertyChanged(nameof(this.IsServiceAvailable));
            }
        }
    }
}
