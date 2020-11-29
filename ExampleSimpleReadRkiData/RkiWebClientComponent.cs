using Newtonsoft.Json;
using System;
using System.Net;

namespace ExampleSimpleReadRkiData
{
    public class RkiWebClientComponent
    {
        public static RkiDataResult LoadAktualData()
        {
            var client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            try
            {
                var urlAddress = "https://services7.arcgis.com/mOBPykOjAyBO2ZKk/arcgis/rest/services/RKI_Landkreisdaten/FeatureServer/0/query?where=1%3D1&outFields=GEN,cases7_per_100k&returnGeometry=false&outSR=4326&f=json";
                var result = client.DownloadString(urlAddress);

                return JsonConvert.DeserializeObject<RkiDataResult>(result);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
