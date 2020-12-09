using ExampleReadAndShowRkiData.Rki;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace ExampleReadAndShowRkiData
{
    internal class CommandSelectAllCountyWihtLast3DaysUps : ICommand
    {
        private MainWindowViewModel _viewModel;

        public CommandSelectAllCountyWihtLast3DaysUps(MainWindowViewModel viewModel) => this._viewModel = viewModel;

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => true;
        public void Execute(object parameter)
        {
            var lastDays = DateTime.Today.AddDays(-3);
            var analyse = new List<string>();
            foreach (var name in this._viewModel
                .RawResultDistricts
                .districts
                .Select(s => s.name))
            {
                var countyResults = HelperExtension.GetCountyResults(name);
                countyResults = countyResults.Where(w =>
                {
                    var d = HelperExtension.InternalTryParse(w.Date);
                    return d > lastDays;
                });

                var isScaleUp = true;
                RkiCovidApiDistrictItem crTemp = null; 
                foreach (var cr in countyResults)
                {
                    if(crTemp == null)
                    {
                        crTemp = cr;
                        continue;
                    }

                    if(cr.weekIncidence < crTemp.weekIncidence)
                    {
                        isScaleUp = false;
                        break;
                    }
                }

                if(isScaleUp)
                {
                    analyse.Add(name);
                }
            }

            foreach (var district in this._viewModel.Districts)
            {
                if(!analyse.Any(a => district.Name.Equals(a)))
                {
                    district.IsPicket = false;
                    continue;
                }

                district.IsPicket = true;
            }
        }
    }
}