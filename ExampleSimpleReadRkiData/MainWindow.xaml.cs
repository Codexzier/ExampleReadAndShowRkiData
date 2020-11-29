using System.Linq;
using System.Windows;

namespace ExampleSimpleReadRkiData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => this.InitializeComponent();

        public override void OnApplyTemplate()
        {
            var rkiData = RkiWebClientComponent.LoadAktualData();

            this.LandkreiseListe.ItemsSource = rkiData
                .features
                .OrderByDescending(o => o.attributes.cases7_per_100k)
                .Select(s => s.attributes);
        }
    }
}
