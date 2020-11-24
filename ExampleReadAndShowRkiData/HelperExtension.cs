using ExampleReadAndShowRkiData.Rki;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ExampleReadAndShowRkiData
{
    public static class HelperExtension
    {
        public static string BuildString(this RkiCovidApiCountryItem item)
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

        internal static IEnumerable<RkiCovidApiCountryItem> GetCountryResults(string name)
        {
            var list = new List<RkiCovidApiCountryItem>();
            foreach (var filename in GetFiles())
            {
                var result = new RkiCovidApiComponent()
                    .LoadAktualData(filename);
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

            return list;

            ;
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
    }
}