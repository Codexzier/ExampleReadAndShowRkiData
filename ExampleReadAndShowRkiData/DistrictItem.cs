using ExampleReadAndShowRkiData.Rki;

namespace ExampleReadAndShowRkiData
{
    public class DistrictItem
    {
        private RkiCovidApiCountryItem _item;

        public DistrictItem(RkiCovidApiCountryItem item)
        {
            this.Name = item.name;
            this.WeekIncidence = item.weekIncidence;
            this.Deaths = item.deaths;
        }

        public string Name { get; }

        public double WeekIncidence { get; }
        public int Deaths { get; }
    }
}