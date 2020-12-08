using ExampleReadAndShowRkiData.Rki;

namespace ExampleReadAndShowRkiData
{
    public class DistrictItem
    {
        private RkiCovidApiDistrictItem _item;

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