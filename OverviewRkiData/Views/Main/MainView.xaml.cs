using System.Windows.Controls;
using OverviewRkiData.Commands;
using OverviewRkiData.Components.Data;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Components.UserSettings;

namespace OverviewRkiData.Views.Main
{
    public partial class MainView : UserControl
    {
        private readonly MainViewModel _viewModel;

        public MainView()
        {
            this.InitializeComponent();

            this._viewModel = (MainViewModel)this.DataContext;

            EventbusManager.Register<MainView, BaseMessage>(this.BaseMessageEvent);
        }

        private void BaseMessageEvent(IMessageContainer arg)
        {
            var settings = UserSettingsLoader.GetInstance().Load();
            // TODO You can use save settings to reload last save setting.
        }

    }
}
