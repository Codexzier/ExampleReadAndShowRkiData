using OverviewRkiData.Views.Base;

namespace OverviewRkiData.Views.Setup
{
    internal class SetupViewModel : BaseViewModel
    {
        private string _serviceAddress;
        private int _port;

        public string ServiceAddress
        {
            get => this._serviceAddress;
            set
            {
                this._serviceAddress = value;
                this.OnNotifyPropertyChanged(nameof(this.ServiceAddress));
            }
        }

        public int Port
        {
            get => this._port;
            set
            {
                this._port = value;
                this.OnNotifyPropertyChanged(nameof(this.Port));
            }
        }
    }
}