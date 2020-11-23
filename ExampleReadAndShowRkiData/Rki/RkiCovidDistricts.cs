using System.Collections.Generic;

namespace ExampleReadAndShowRkiData.Rki
{
    public class RkiCovidDistricts
    {
        public string lastUpdate { get; set; }
        public IList<RkiCovidApiCountryItem> districts { get; set; }
    }

}
