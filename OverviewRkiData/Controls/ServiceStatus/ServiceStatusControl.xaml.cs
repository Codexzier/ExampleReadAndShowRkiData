using System.Windows;
using System.Windows.Controls;

namespace OverviewRkiData.Controls.ServiceStatus
{
    /// <summary>
    /// Interaction logic for ServiceStatusControl.xaml
    /// </summary>
    public partial class ServiceStatusControl : UserControl
    {
        public bool ServiceState
        {
            get => (bool)this.GetValue(ServiceSateProperty);
            set => this.SetValue(ServiceSateProperty, value);
        }

        // Using a DependencyProperty as the backing store for ServiceSate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ServiceSateProperty =
            DependencyProperty.RegisterAttached("ServiceSate",
                typeof(bool),
                typeof(ServiceStatusControl),
                new PropertyMetadata(false, ServiceStateChangedCallBack));

        private static void ServiceStateChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ServiceStatusControl control && e.NewValue is bool b)
            {
                control.ProgressBar.IsIndeterminate = b;
            }
        }

        public ServiceStatusControl() => this.InitializeComponent();
    }
}
