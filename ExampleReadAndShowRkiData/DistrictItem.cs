using ExampleReadAndShowRkiData.Rki;

namespace ExampleReadAndShowRkiData
{
    public class DistrictItem
    {
        public DistrictItem(RkiCovidApiDistrictItem item)
        {
            this.Name = item.name;
            this.WeekIncidence = item.weekIncidence;
            this.Deaths = item.deaths;
        }

        public string Name { get; }

        public double WeekIncidence { get; }
        public int Deaths { get; }

        public bool IsPicket { get; set; }
    }
}