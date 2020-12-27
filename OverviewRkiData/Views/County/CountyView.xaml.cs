using OverviewRkiData.Commands;
using OverviewRkiData.Components.Data;
using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Controls.Diagram;
using OverviewRkiData.Views.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OverviewRkiData.Views.County
{
    /// <summary>
    /// Interaction logic for CountyView.xaml
    /// </summary>
    public partial class CountyView : UserControl
    {
        private readonly CountyViewModel _viewModel;

        public CountyView()
        {
            this.InitializeComponent();

            this._viewModel = (CountyViewModel)this.DataContext;

            EventbusManager.Register<CountyView, BaseMessage>(this.CountyMessageEvent);
        }

        private async void CountyMessageEvent(IMessageContainer obj)
        {
            if(obj.Content is DistrictItem districtItem)
            {
                this._viewModel.DistrictData = districtItem;

                await Task.Run(() =>
                {
                    var result = HelperExtension.GetCountyResults(districtItem.Name);

                    this._viewModel.CountyResults = result.Select(s => new DiagramLevelItem { Value = s.WeekIncidence, ToolTipText = "??" }).ToList();
                });
            }
        }

        //internal static IEnumerable<DistrictItem> GetCountyResults(string name)
        //{
        //    var list = new List<DistrictItem>();
        //    foreach (var filename in GetFiles())
        //    {
        //        var result = RkiCoronaLandkreiseComponent.GetInstance()
        //            .LoadFromFile(filename);
        //        var v = result
        //            .Districts
        //            .FirstOrDefault(w => w.Name.Equals(name));

        //        if (v == null)
        //        {
        //            continue;
        //        }

        //        list.Add(new DistrictItem { Name = v.Name, Deaths = v.Deaths, WeekIncidence = v.WeekIncidence, Date = result.Date });
        //    }

        //    return list.OrderBy(o => o.Date).ToList();
        //}



        //private static IEnumerable<string> GetFiles()
        //{
        //    return Directory
        //        .GetFiles(Environment.CurrentDirectory)
        //        .Where(w => w.EndsWith(".json") &&
        //                    w.Contains(HelperExtension.RkiFilename));
        //}
    }
}
