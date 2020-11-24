using ExampleReadAndShowRkiData.Rki;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExampleReadAndShowRkiData.Controls
{
    /// <summary>
    /// Interaction logic for DiagramControl.xaml
    /// </summary>
    public partial class DiagramControl : UserControl
    {


        public List<RkiCovidApiCountryItem> RkiCountryData { 
            get => (List<RkiCovidApiCountryItem>)this.GetValue(RkiCountryDataProperty); 
            set => this.SetValue(RkiCountryDataProperty, value); 
        }

        public static readonly DependencyProperty RkiCountryDataProperty =
            DependencyProperty.RegisterAttached("RkiCountryData",
                typeof(List<RkiCovidApiCountryItem>),
                typeof(DiagramControl),
                new PropertyMetadata(new List<RkiCovidApiCountryItem>(), UpdateDiagram));
        
        private static void UpdateDiagram(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DiagramControl control)
            {
                if(control.RkiCountryData == null)
                {
                    return;
                }

                control.SimpleDiagram.Children.Clear();

                var widthPerResult =  control.MaxWidth / control.RkiCountryData.Count;

                foreach (var item in control.RkiCountryData)
                {
                    var rect = new System.Windows.Shapes.Rectangle
                    {
                        Fill = new SolidColorBrush(Colors.Green),
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Width = widthPerResult,
                        Height = item.weekIncidence / 2.5d,
                        ToolTip = $"{item.Date}, 7 Tage Inzidenz: {item.weekIncidence:N1}, Tote: {item.deaths}"
                    };
                    control.SimpleDiagram.Children.Add(rect);    
                }
            }
        }

        public DiagramControl() => this.InitializeComponent();
    }
}
