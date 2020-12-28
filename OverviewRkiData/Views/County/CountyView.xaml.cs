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

                    this._viewModel.CountyResults = result.Select(s =>
                    {

                        var toolTip = $"{s.Date:d} | {s.WeekIncidence:N1} | {s.Deaths}";
                        return new DiagramLevelItem { Value = s.WeekIncidence, ToolTipText = toolTip };
                    }).ToList();
                });
            }
        }
    }
}
