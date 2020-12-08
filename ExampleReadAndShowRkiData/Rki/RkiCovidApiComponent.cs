using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;

namespace ExampleReadAndShowRkiData.Rki
{
    internal class RkiCovidApiComponent
    {
        private readonly string _address = "https://rki-covid-api.now.sh/api/";

        internal RkiCovidApiDistricts LoadData()
        {
            var filename = HelperExtension.CreateFilename();

            if (File.Exists(filename))
            {
                var t = this.LoadFromFile(filename);
                var str = t.lastUpdate.RemoveTimeFromLastUpdateString();

                if (DateTime.TryParse(str, out DateTime dt))
                {
                    var actualDateTime = DateTime.Now.ToShortDateString();
                    var lastUpdateTime = dt.ToShortDateString();

                    if (actualDateTime.Equals(lastUpdateTime))
                    {
                        return this.LoadFromFile(filename);
                    }
                }
            }

            return this.LoadAktualDataFromInternet(filename);
        }

        private RkiCovidApiDistricts LoadAktualDataFromInternet(string filename)
        {
            var client = new WebClient { BaseAddress = this._address };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            try
            {
                var urlAddress = $"{this._address}districts";
                var result = client.DownloadString(urlAddress);

                File.WriteAllText(filename, result);

                return JsonConvert.DeserializeObject<RkiCovidApiDistricts>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        internal RkiCovidApiDistricts LoadFromFile(string filename)
        {
            return JsonConvert.DeserializeObject<RkiCovidApiDistricts>(File.ReadAllText(filename));
        }
    }

}
