using Newtonsoft.Json;
using OverviewRkiData.Components.Data;
using OverviewRkiData.Components.UserSettings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace OverviewRkiData.Components.RkiCoronaLandkreise
{
    public class RkiCoronaLandkreiseComponent
    {
        private static RkiCoronaLandkreiseComponent _singelton;

        private readonly string _urlGenCasesDeathsWeekinzidenz = "https://services7.arcgis.com/mOBPykOjAyBO2ZKk/arcgis/rest/services/RKI_Landkreisdaten/FeatureServer/0/query?where=1%3D1&outFields=cases,deaths,county,last_update,cases7_per_100k,death_rate,GEN&returnGeometry=false&outSR=4326&f=json";

        private RkiCoronaLandkreiseComponent() { }

        public static RkiCoronaLandkreiseComponent GetInstance()
        {
            if (_singelton == null)
            {
                _singelton = new RkiCoronaLandkreiseComponent();
            }
            return _singelton;
        }

        public Landkreise LoadData()
        {
            var filename = HelperExtension.CreateFilename();

            if (!UserSettingsLoader.GetInstance().Load().LoadRkiDataByApplicationStart)
            {
                filename = this.GetLastLoadedData();
            }

            if (!string.IsNullOrEmpty(filename) && File.Exists(filename))
            {
                var reload = this.LoadLocalOrReloadOnlineFromRki(filename);
                if (reload != null)
                {
                    return reload;
                }
            }

            var result = this.ConvertToLandkreise(this.LoadAktualData());
            this.SaveToFile(result, filename);
            return result;
        }

        private string GetLastLoadedData()
        {
            var last = HelperExtension.GetRkiFiles().Select(s => new FileInfo(s)).OrderBy(w =>
            {
                var dateStr = w.FullName.GetDate();
                if (DateTime.TryParse(dateStr, out var dt))
                {
                    return dt;
                }

                return DateTime.MinValue;
            });

            if(!last.Any())
            {
                return string.Empty;
            }

            return last.Last().FullName;
        }

        private Landkreise LoadLocalOrReloadOnlineFromRki(string filename)
        {
            var t = this.LoadFromFile(filename);

            var actualDateTime = DateTime.Now.ToShortDateString();
            var lastUpdateTime = t.Date.ToShortDateString();

            if (actualDateTime.Equals(lastUpdateTime))
            {
                var resultFromFile = this.LoadFromFile(filename);

                if (resultFromFile
                    .Date
                    .ToShortTimeString()
                    .Equals(actualDateTime))
                {
                    return resultFromFile;
                }
            }

            return null;
        }

        private Landkreise ConvertToLandkreise(RkiCoronaLandkreiseResult result)
        {
            var lk = result.features.FirstOrDefault();
            if (lk == null)
            {
                return new Landkreise();
            }

            var strLastUpdate = lk.attributes.last_update.RemoveTimeFromLastUpdateString();
            if (DateTime.TryParse(strLastUpdate, out var lastUpdate))
            {
                var landkreise = new Landkreise
                {
                    Date = lastUpdate,
                    Districts = new List<Landkreis>()
                };

                foreach (var item in result.features)
                {
                    landkreise.Districts.Add(new Landkreis
                    {
                        Name = item.attributes.GEN,
                        WeekIncidence = item.attributes.cases7_per_100k,
                        Cases = item.attributes.cases,
                        Deaths = item.attributes.deaths
                    });
                }

                return landkreise;
            }

            return new Landkreise();
        }

        internal Landkreise LoadFromFile(string filename)
        {
            return JsonConvert.DeserializeObject<Landkreise>(File.ReadAllText(filename));
        }

        internal void SaveToFile(Landkreise landkreise, string filename)
        {
            var contentStr = JsonConvert.SerializeObject(landkreise);

            File.WriteAllText(filename, contentStr);
        }

        private RkiCoronaLandkreiseResult LoadAktualData()
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            try
            {
                var result = client.DownloadString(this._urlGenCasesDeathsWeekinzidenz);

                return JsonConvert.DeserializeObject<RkiCoronaLandkreiseResult>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
