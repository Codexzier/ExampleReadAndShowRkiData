namespace ExampleReadAndShowRkiData.Rki
{
    public class RkiCovidApiCountryItem
    {
        public string name { get; set; }
        public string country { get; set; }
        public int count { get; set; }
        public int deaths { get; set; }
        public double weekIncidence { get; set; }
        public double casesPer100k { get; set; }
        public double casesPerPopulation { get; set; }
    }

}
