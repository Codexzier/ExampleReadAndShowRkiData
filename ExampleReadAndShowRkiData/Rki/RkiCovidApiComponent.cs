using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExampleReadAndShowRkiData.Rki
{
    internal class RkiCovidApiComponent
    {
        private readonly string _address = "https://rki-covid-api.now.sh/api/";

        public RkiCovidApiDistricts LoadAktualData(bool _updateDataFromInternet)
        {
            var filename = HelperExtension.CreateFilename();

            if (File.Exists(filename) && !_updateDataFromInternet)
            {
                return this.LoadAktualData(filename);
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

        internal RkiCovidApiDistricts LoadAktualData(string filename)
        {
            return JsonConvert.DeserializeObject<RkiCovidApiDistricts>(File.ReadAllText(filename));
        }
    }

}
