using Newtonsoft.Json;
using OverviewRkiData.Components.Data;
using OverviewRkiData.Components.RkiCoronaLandkreise;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace OverviewRkiData.Components.LegacyData
{
    internal class LegacyDataConverter
    {
        private readonly string _path = "C:\\Users\\johan\\OneDrive\\Anwendungen\\JPL WPF Demos\\ExampleRkiReader";

        public void Run()
        {
            var newComponent = RkiCoronaLandkreiseComponent.GetInstance();
            var files = this.GetRkiFiles();

            foreach (var item in files)
            {
                var legacyData = JsonConvert.DeserializeObject<LegacyDataFormat>(File.ReadAllText(item));

                var date = legacyData.lastUpdate.RemoveTimeFromLastUpdateString();

                var newFilename = $"{Environment.CurrentDirectory}/{HelperExtension.RkiFilename}-{date}.json";

                if (File.Exists(newFilename))
                {
                    continue;
                }

                var landkreise = new Landkreise();
                var d = DateTime.Parse(date);
                landkreise.Date = d;
                landkreise.Districts = legacyData.districts.Select(s =>
                    new Landkreis 
                    { 
                        Name = s.name, 
                        Date = d,
                        Deaths = s.deaths, 
                        WeekIncidence = s.weekIncidence 
                    }
                ).ToList();

                newComponent.SaveToFile(landkreise, newFilename);
            }
        }

        private IEnumerable<string> GetRkiFiles()
        {
            var files = Directory
                .GetFiles(this._path)
                .Where(w => w.Contains(HelperExtension.RkiFilename));

            return files;
        }
    }

    public class LegacyDataFormat
    {
        public string lastUpdate { get; set; }
        public IList<LegacyDistrictItem> districts { get; set; }
    }
    public class LegacyDistrictItem
    {
        public string name { get; set; }
        public string county { get; set; }
        public int count { get; set; }
        public int deaths { get; set; }
        public double weekIncidence { get; set; }
        public double casesPer100k { get; set; }
        public double casesPerPopulation { get; set; }
        public string Date { get; internal set; }
    }

}
