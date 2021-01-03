﻿using OverviewRkiData.Components.RkiCoronaLandkreise;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace OverviewRkiData.Components.Data
{
    public static class HelperExtension
    {
        internal static string DataFolderName = "rki-data";
        internal static string RkiFilename = "rki-corona-data";

        public static string CreateFilename()
        {
            var folder = SubFolderRkiData();
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var date = DateTime.Today;

            return $"{folder}/{RkiFilename}-{date:d}.json";
        }

        public static string SubFolderRkiData() => $"{Environment.CurrentDirectory}/{DataFolderName}";

        public static string GetDate(this string filename)
        {
            var strArray = filename.Split("\\").Last();

            var date = strArray.Substring(RkiFilename.Length + 1);
            date = date.Remove(date.Length - 5);

            return date;
        }

        internal static IEnumerable<Landkreis> GetCountyResults(string name)
        {
            var list = new List<Landkreis>();
            foreach (var filename in GetFiles())
            {
                var result = RkiCoronaLandkreiseComponent.GetInstance()
                    .LoadFromFile(filename);
                var v = result
                    .Districts
                    .FirstOrDefault(w => w.Name.Equals(name));

                if (v == null)
                {
                    continue;
                }

                v.Date = result.Date;
                list.Add(v);
            }

            return list.OrderBy(o => o.Date).ToList();
        }

        internal static string RemoveTimeFromLastUpdateString(this string lastUpdate)
        {
            return lastUpdate.Split(',')[0];
        }

        //public static IEnumerable<string> GetRkiFiles()
        //{
        //    var files = Directory
        //        .GetFiles(SubFolderRkiData())
        //        .Where(w => w.Contains(RkiFilename));

        //    return files;
        //}

        internal static IEnumerable<string> GetFiles()
        {
            return Directory
                .GetFiles(SubFolderRkiData())
                .Where(w => w.EndsWith(".json") &&
                            w.Contains(HelperExtension.RkiFilename));
        }

        internal static IEnumerable<Landkreis> GetCountyResultsByPicketItems(IEnumerable<string> pickedNames)
        {
            var collection = new List<Landkreis>();

            foreach (var item in pickedNames)
            {
                collection.AddRange(GetCountyResults(item));
            }

            var gg = collection.GroupBy(gb => gb.Date);

            var result = gg.Select(s =>
            {
                var t = s.Count();
                Debug.WriteLine($"Anzahl: {t}");

                var summeWeekIncidence = s.Sum(s => s.WeekIncidence);
                var summeDeath = s.Sum(s => s.Deaths);

                return new Landkreis
                {
                    WeekIncidence = summeWeekIncidence,
                    Deaths = summeDeath,
                    Date = s.Key
                };
            });

            return result.ToList();
        }

    }
}
