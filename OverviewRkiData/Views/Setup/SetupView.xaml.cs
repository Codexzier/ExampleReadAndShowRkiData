using OverviewRkiData.Components.Ui.Eventbus;
using OverviewRkiData.Components.UserSettings;
using OverviewRkiData.Views.Base;
using OverviewRkiData.Views.MessageBox;
using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace OverviewRkiData.Views.Setup
{
    /// <summary>
    /// Interaction logic for SetupView.xaml
    /// </summary>
    public partial class SetupView : UserControl
    {
        private readonly SetupViewModel _viewModel;
        public SetupView()
        {
            this.InitializeComponent();

            this._viewModel = (SetupViewModel)this.DataContext;

            this._viewModel.CommandLoadRkiDataByApplicationStart = new CheckBoxCommandLoadRkiDataByApplicationStart(viewModel: this._viewModel);
            this._viewModel.CommandImportDataFromLegacyApplication = new ButtonCommandImportDataFromLegacyApplication(this._viewModel);
        }

        public override void OnApplyTemplate()
        {
            var setting = UserSettingsLoader.GetInstance().Load();
            this._viewModel.LoadRkiDataByApplicationStart = setting.LoadRkiDataByApplicationStart;
        }
               
    }
}
