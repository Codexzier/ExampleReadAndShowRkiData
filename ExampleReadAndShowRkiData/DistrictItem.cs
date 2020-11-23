using ExampleReadAndShowRkiData.Rki;

namespace ExampleReadAndShowRkiData
{
    public class DistrictItem
    {
        public DistrictItem(RkiCovidApiCountryItem item)
        {
            this.Name = item.name;
            this.WeekIncidence = item.weekIncidence;
        }

        public string Name { get; set; }

        public double WeekIncidence { get; set; }
    }
}