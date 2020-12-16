using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using OverviewRkiData.Commands;
using OverviewRkiData.Components;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.Main;
using OverviewRkiData.Views.Setup;

namespace OverviewRkiData.Views.Menu
{
    public partial class MenuView : UserControl
    {
        private readonly MenuViewModel _viewModel;

        private readonly DispatcherTimer _checkIsServiceAvailbale = new DispatcherTimer();

        public MenuView()
        {
            this.InitializeComponent();

            this._viewModel = (MenuViewModel)this.DataContext;

            EventbusManager.Register<MenuView, BaseMessage>(this.BaseMessageEvent);

            this._viewModel.ViewOpened = EventbusManager.GetViewOpened(0);

            this._checkIsServiceAvailbale.Tick += this._checkIsServiceAvailbale_Tick;
            this._checkIsServiceAvailbale.Interval = TimeSpan.FromSeconds(10);
            //this._checkIsServiceAvailbale.Start();
        }

        private void _checkIsServiceAvailbale_Tick(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                var service = ComponentFactory.GetServiceConnector();
                var result = await service.GetDvdItems();

                if (result.Success)
                {
                    this.Dispatcher.Invoke(() => { this._viewModel.IsServiceAvailable = result.Success; });
                }
            });
        }

        private void BaseMessageEvent(IMessageContainer arg)
        {
            // do things with the content
        }

        private void ButtonOpenMain_Click(object sender, RoutedEventArgs e)
        {
            if (EventbusManager.IsViewOpen(typeof(MainView), 0))
            {
                return;
            }

            EventbusManager.OpenView<MainView>(0);
            this._viewModel.ViewOpened = EventbusManager.GetViewOpened(0);
            EventbusManager.Send<MainView, BaseMessage>(new BaseMessage(""), 0);
        }

        private void ButtonSetup_Click(object sender, RoutedEventArgs e)
        {
            if (EventbusManager.IsViewOpen(typeof(SetupView), 0))
            {
                return;
            }

            EventbusManager.OpenView<SetupView>(0);
            this._viewModel.ViewOpened = EventbusManager.GetViewOpened(0);
        }
    }
}
