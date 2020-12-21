using OverviewRkiData.Components.Data;
using OverviewRkiData.Views.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OverviewRkiData.Views.Main
{
    public class MainViewModel : BaseViewModel
    {
        public ObservableCollection<DistrictItem> Districts { get; set; }
    }
}
