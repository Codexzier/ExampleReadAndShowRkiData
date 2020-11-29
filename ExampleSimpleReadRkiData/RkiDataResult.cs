namespace ExampleSimpleReadRkiData
{
    public class RkiDataResult
    {
        public Landkreis[] features { get; set; }

        public class Landkreis
        {
            public Attribute attributes { get; set; }

            public class Attribute
            {
                public string GEN { get; set; }

                public double cases7_per_100k { get; set; }
            }
        }
    }
}
