using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Components.UserSettings;
using OverviewRkiData.Views.Base;
using OverviewRkiData.Views.MessageBox;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace OverviewRkiData.Views.Setup
{
    /// <summary>
    /// Interaction logic for SetupView.xaml
    /// </summary>
    public partial class SetupView : UserControl, IDisposable
    {
        private readonly SetupViewModel _viewModel;
        public SetupView()
        {
            this.InitializeComponent();

            this._viewModel = (SetupViewModel)this.DataContext;
        }

        public override void OnApplyTemplate()
        {
            var setting = UserSettingsLoader.GetInstance().Load();

            this._viewModel.ServiceAddress = setting.ServiceAddress;
            this._viewModel.Port = setting.Port;
        }

        public void Dispose()
        {
            EventbusManager.Deregister<SetupView>();
        }

        private void ButtonSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            var userSetting = UserSettingsLoader.GetInstance();
            var setting = userSetting.Load();

            if (Regex.IsMatch(this._viewModel.ServiceAddress, @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&//=]*)"))
            {
                SimpleMessageBox.Show("ERROR", "Check address. Address must locks like http://localhost");
                // EventbusManager.Send<MessageBoxView, MessageBoxMessage>(new MessageBoxMessage("ERROR", "Check address. Address must locks like http://localhost"), 10, true);
                return;
            }

            setting.ServiceAddress = this._viewModel.ServiceAddress;
            setting.Port = this._viewModel.Port;

            userSetting.Save(setting);
        }
    }
}
