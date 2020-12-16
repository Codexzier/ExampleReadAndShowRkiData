using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.MessageBox;
using System.Windows;

namespace OverviewRkiData.Views.Base
{
    public static class SimpleMessageBox
    {
        public static void Show(string title, string message)
        {
            Application.Current.Dispatcher.Invoke(delegate
            {
                EventbusManager.Send<MessageBoxView, MessageBoxMessage>(new MessageBoxMessage(title, message), 10, true);
            });
        }
    }
}
