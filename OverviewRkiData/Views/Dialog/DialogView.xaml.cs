using System.Windows.Controls;
using OverviewRkiData.Commands;
using OverviewRkiData.Components.Data;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Views.DialogContent;

namespace OverviewRkiData.Views.Dialog
{
    /// <summary>
    /// Interaction logic for DialogView.xaml
    /// </summary>
    public partial class DialogView : UserControl
    {
        private readonly DialogViewModel _viewModel;
        private object _content = null;

        public DialogView()
        {
            this.InitializeComponent();

            this._viewModel = (DialogViewModel)this.DataContext;

            EventbusManager.Register<DialogView, BaseMessage>(this.BaseMessageEvent);

            this.Loaded += this.DialogView_Loaded;
        }

        ~DialogView()
        {
            this.Loaded -= this.DialogView_Loaded;
        }

        private void DialogView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this._content is CommonData)
            {
                EventbusManager.OpenView<DialogContentView>(2);
                EventbusManager.Send<DialogContentView, BaseMessage>(new BaseMessage(this._content), 2);
            }
        }

        private void BaseMessageEvent(IMessageContainer obj)
        {
            if (!(obj.Content is DataDialogContent dialogContent))
            {
                return;
            }

            this._viewModel.Header = dialogContent.Header;
            this._content = dialogContent.Content;
        }
    }
}
