﻿using ExampleReadAndShowRkiData.Rki;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ExampleReadAndShowRkiData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowViewModel _viewModel;
        public MainWindow()
        {
            this.InitializeComponent();

            this._viewModel = (MainWindowViewModel)this.DataContext;

            this._viewModel.SortByWeekIncidence = new CommandSortByWeekIncidence(this._viewModel);
            this._viewModel.SortByDeath = new CommandSortByDeath(this._viewModel);
            this._viewModel.LoadData = new CommandLoadData(this._viewModel);
            this._viewModel.ListBoxSelectionChanged = new CommandListBoxSelectionChanged(this._viewModel);
            this._viewModel.SelectAllCountyWihtLast3DaysUps = new CommandSelectAllCountyWihtLast3DaysUps(this._viewModel);
        }

        public override void OnApplyTemplate()
        {
            var files = HelperExtension
                .GetFiles()
                .Select(s => new RkiJsonResultItem(s))
                .OrderByDescending(o => o.Date);

            this._viewModel.JsonFiles = new ObservableCollection<RkiJsonResultItem>(files);
            this._viewModel.SelectedDateRkiJsonResult = this._viewModel.JsonFiles.FirstOrDefault();
        }

        private void TextBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(this._viewModel.RawResultDistricts == null)
            {
                return;
            }

            if (string.IsNullOrEmpty(this._viewModel.SearchContains))
            {
                var defaultList = this._viewModel
                    .RawResultDistricts
                    .districts
                    .Select(s => new DistrictItem(s));

                this._viewModel.Districts = new ObservableCollection<DistrictItem>(defaultList);
                
                return;
            }

            var searchResult = this._viewModel
                .RawResultDistricts
                .districts
                .Where(w => w
                    .name
                    .ToLower()
                    .Contains(this._viewModel.SearchContains.ToLower()))
                .Select(s => new DistrictItem(s));

            this._viewModel.Districts = new ObservableCollection<DistrictItem>(searchResult);
        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (this._viewModel.SelectedDistrict == null || string.IsNullOrEmpty(this._viewModel.SelectedDistrict.Name))
            {
                return;
            }

            this._viewModel.DistrictData = this._viewModel
                .RawResultDistricts
                .districts
                .FirstOrDefault(district => district
                    .name
                    .ToLower()
                    .Equals(this._viewModel.SelectedDistrict.Name.ToLower()));

            this._viewModel.CountyResults = HelperExtension.GetCountyResults(this._viewModel.SelectedDistrict.Name);

            this.ReloadSelected();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(this._viewModel.SelectedDateRkiJsonResult == null)
            {
                return;
            }

            var result = new RkiCovidApiComponent().LoadFromFile(this._viewModel.SelectedDateRkiJsonResult.Filename);

            this._viewModel.LastUpdate = result.lastUpdate.RemoveTimeFromLastUpdateString();
            this._viewModel.RawResultDistricts = result;
            this._viewModel.Districts = new ObservableCollection<DistrictItem>(result.districts.Select(s => new DistrictItem(s)));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            this.ReloadSelected();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            this.ReloadSelected();
        }

        private void ReloadSelected()
        {
            var pickedNames = this._viewModel.Districts.Where(w => w.IsPicket).Select(s => s.Name);
            if (!pickedNames.Any())
            {
                this._viewModel.PicketResults = new List<RkiCovidApiDistrictItem>();
                return;
            }
            this._viewModel.PicketResults = HelperExtension.GetCountyResultsByPicketItems(pickedNames);
        }
    }
}
