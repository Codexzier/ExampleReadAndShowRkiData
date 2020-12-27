using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.ActivityLoading;
using OverviewRkiData.Views.MessageBox;
using System.Windows;

namespace OverviewRkiData.Views.Base
{
    public static class SimpleStatusOverlays
    {
        public static void Show(string title, string message)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                EventbusManager.Send<MessageBoxView, MessageBoxMessage>(new MessageBoxMessage(title, message), 10, true);
            });
        }

        public static void ActivityOn()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                EventbusManager.OpenView<ActivityLoadingView>(10);
            });
        }

        public static void ActivityOff()
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                EventbusManager.CloseView<ActivityLoadingView>(10);
            });
        }
    }
}
