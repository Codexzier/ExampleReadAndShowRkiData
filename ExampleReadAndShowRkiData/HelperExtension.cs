using ExampleReadAndShowRkiData.Rki;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ExampleReadAndShowRkiData
{
    public static class HelperExtension
    {
        public static string BuildString(this RkiCovidApiDistrictItem item)
        {
            return $"{item.name} - {item.weekIncidence:N2}";
        }

        internal static string RkiFilename = "rki-corona-data";

        public static string CreateFilename()
        {
            var date = DateTime.Today;

            return $"{Environment.CurrentDirectory}/{RkiFilename}-{date:d}.json";
        }

        public static string CreateFilenameCountry(string country)
        {
            return $"{Environment.CurrentDirectory}/{RkiFilename}-{country}.json";
        }

        public static string GetDate(this string filename)
        {
            var strArray = filename.Split("\\").Last();

            var date = strArray.Substring(RkiFilename.Length + 1);
            date = date.Remove(date.Length - 5);

            return date;
        }

        internal static IEnumerable<RkiCovidApiDistrictItem> GetCountyResults(string name)
        {
            var list = new List<RkiCovidApiDistrictItem>();
            foreach (var filename in GetFiles())
            {
                var result = new RkiCovidApiComponent()
                    .LoadFromFile(filename);
                var v = result
                    .districts
                    .FirstOrDefault(w => w.name.Equals(name));

                if(v == null)
                {
                    continue;
                }

                v.Date = result.lastUpdate.RemoveTimeFromLastUpdateString();
                list.Add(v);
            }

            return list.OrderBy(o => {
            if (DateTime.TryParse(o.Date, out DateTime dt)){
                return dt;
            }

                return DateTime.MinValue;
            }).ToList();
        }

        internal static string RemoveTimeFromLastUpdateString(this string lastUpdate)
        {
            return lastUpdate.Split(',')[0];
        }

        internal static IEnumerable<string> GetFiles()
        {
            return Directory
                .GetFiles(Environment.CurrentDirectory)
                .Where(w => w.EndsWith(".json") &&
                            w.Contains(HelperExtension.RkiFilename));
        }

        internal static IEnumerable<RkiCovidApiDistrictItem> GetCountyResultsByPicketItems(IEnumerable<string> pickedNames)
        {
            var collection = new List<RkiCovidApiDistrictItem>();

            foreach (var item in pickedNames)
            {
                collection.AddRange(GetCountyResults(item));
            }

            var gg = collection.GroupBy(gb => InternalTryParse(gb.Date));

            var result = gg.Select(s =>
            {
                var t = s.Count();
                Debug.WriteLine($"Anzahl: {t}");

                var summeWeekIncidence = s.Sum(s => s.weekIncidence);
                var summeDeath = s.Sum(s => s.deaths);

                return new RkiCovidApiDistrictItem 
                { 
                    weekIncidence = summeWeekIncidence, 
                    deaths = summeDeath ,
                    Date = s.Key.ToShortDateString()
                };
            });
                        
            return result.ToList();
        }

        internal static DateTime InternalTryParse(string date)
        {
            if (DateTime.TryParse(date, out DateTime dt))
            {
                return dt;
            }

            return DateTime.MinValue;
        }
    }
}