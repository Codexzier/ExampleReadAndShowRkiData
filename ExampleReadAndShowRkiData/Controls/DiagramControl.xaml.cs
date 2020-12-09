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


        public static int GetMyProperty(DependencyObject obj)
        {
            return (int)obj.GetValue(ScaleProperty);
        }

        public static void SetMyProperty(DependencyObject obj, int value)
        {
            obj.SetValue(ScaleProperty, value);
        }

        public double Scale 
        {
            get => (double)this.GetValue(ScaleProperty);
            set => this.SetValue(ScaleProperty, value);
        }

       
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.RegisterAttached(
                "Scale", 
                typeof(double), 
                typeof(DiagramControl), 
                new PropertyMetadata(2.5, UpdateDiagram));

        public List<RkiCovidApiDistrictItem> RkiCountyData { 
            get => (List<RkiCovidApiDistrictItem>)this.GetValue(RkiCountyDataProperty); 
            set => this.SetValue(RkiCountyDataProperty, value); 
        }

        public static readonly DependencyProperty RkiCountyDataProperty =
            DependencyProperty.RegisterAttached("RkiCountyData",
                typeof(List<RkiCovidApiDistrictItem>),
                typeof(DiagramControl),
                new PropertyMetadata(new List<RkiCovidApiDistrictItem>(), UpdateDiagram));
        
        private static void UpdateDiagram(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is DiagramControl control)
            {
                if(control.RkiCountyData == null)
                {
                    return;
                }

                control.SimpleDiagram.Children.Clear();

                var widthPerResult =  control.MaxWidth / control.RkiCountyData.Count;

                foreach (var item in control.RkiCountyData)
                {
                    var rect = new System.Windows.Shapes.Rectangle
                    {
                        Fill = new SolidColorBrush(Colors.Green),
                        VerticalAlignment = VerticalAlignment.Bottom,
                        Width = widthPerResult,
                        Height = item.weekIncidence / control.Scale,
                        ToolTip = $"{item.Date}, 7 Tage Inzidenz: {item.weekIncidence:N1}, Tote: {item.deaths}"
                    };
                    control.SimpleDiagram.Children.Add(rect);    
                }
            }
        }

        public DiagramControl() => this.InitializeComponent();
    }
}
