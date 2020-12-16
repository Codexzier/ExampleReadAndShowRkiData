using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OverviewRkiData.Views.DialogContent
{
    /// <summary>
    /// Interaction logic for DialogContentView.xaml
    /// </summary>
    public partial class DialogContentView : UserControl
    {
        private readonly DialogContentViewModel _viewModel;
        public DialogContentView()
        {
            this.InitializeComponent();

            this._viewModel = (DialogContentViewModel)this.DataContext;
        }
    }
}
