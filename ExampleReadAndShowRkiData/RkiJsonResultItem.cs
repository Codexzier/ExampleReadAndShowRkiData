namespace ExampleReadAndShowRkiData
{
    public class RkiJsonResultItem
    {
        public RkiJsonResultItem(string filename)
        {
            this.Filename = filename;
            this.Date = filename.GetDate();
        }

        public string Filename { get; }
        public string Date { get; }

        public override string ToString() => $"{this.Date}";
    }
}