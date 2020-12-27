namespace OverviewRkiData.Components.RkiCoronaLandkreise
{
    public class RkiCoronaLandkreiseResult
    {
        public Landkreis[] features { get; set; }

        public class Landkreis
        {
            public Attribute attributes { get; set; }

            public class Attribute
            {
                public string GEN { get; set; }

                public double cases7_per_100k { get; set; }

                public int deaths { get; set; }

                public int cases { get; set; }

                public string last_update { get; set; }
            }
        }
    }
}