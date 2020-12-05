using System;

namespace ExampleReadAndShowRkiData
{
    public class RkiJsonResultItem
    {
        public RkiJsonResultItem(string filename)
        {
            this.Filename = filename;
            var strDate = filename.GetDate();
            if (DateTime.TryParse(strDate, out DateTime dt))
            {
                this.Date = dt;
            }
        }

        public string Filename { get; }
        public DateTime Date { get; }

        public override string ToString() => $"{this.Date:d}";
    }
}