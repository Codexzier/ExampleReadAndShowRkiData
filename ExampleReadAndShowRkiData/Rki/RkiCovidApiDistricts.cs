using System.Collections.Generic;

namespace ExampleReadAndShowRkiData.Rki
{
    public class RkiCovidApiDistricts
    {
        public string lastUpdate { get; set; }
        public IList<RkiCovidApiDistrictItem> districts { get; set; }
    }

}
