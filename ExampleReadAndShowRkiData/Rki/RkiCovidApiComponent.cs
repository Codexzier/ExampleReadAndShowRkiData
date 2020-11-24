﻿using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExampleReadAndShowRkiData.Rki
{
    internal class RkiCovidApiComponent
    {
        // https://rki-covid-api.now.sh/

        private readonly string _address = "https://rki-covid-api.now.sh/api/";

        public RkiCovidDistricts LoadAktualData(bool _updateDataFromInternet)
        {
            var filename = HelperExtension.CreateFilename();

            if(File.Exists(filename) && !_updateDataFromInternet)
            {
                return this.LoadAktualData(filename);
            }

            var client = new WebClient { BaseAddress = this._address };
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            try
            {
                var urlAddress = $"{this._address}districts";
                var result = client.DownloadString(urlAddress);

                File.WriteAllText(filename, result);

                return JsonConvert.DeserializeObject<RkiCovidDistricts>(result);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        internal RkiCovidDistricts LoadAktualData(string filename)
        {
            return JsonConvert.DeserializeObject<RkiCovidDistricts>(File.ReadAllText(filename));
        }
    }

}
