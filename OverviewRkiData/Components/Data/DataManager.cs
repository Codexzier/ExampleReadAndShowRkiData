using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace OverviewRkiData.Components.Data
{
    public class DataManager
    {
        private readonly string _pathName = $"{Environment.CurrentDirectory}";

        public DataManager()
        {
        }

        private string GetFilename<TItem>()
        {
            return $"{this._pathName}\\{typeof(TItem).Name}.json";
        }

        public bool SaveData<TItem>(IEnumerable<TItem> items)
        {
            var str = JsonConvert.SerializeObject(items);
            string fileName = this.GetFilename<TItem>();

            using (var writer = new StreamWriter(fileName))
            {
                writer.Write(str);
            }

            return true;
        }

        public IEnumerable<TItem> GetData<TItem>()
        {
            string fileName = this.GetFilename<TItem>();

            if (!File.Exists(fileName))
            {
                return new TItem[0];
            }

            string jsonContent = File.ReadAllText(fileName);

            var result = JsonConvert.DeserializeObject<TItem[]>(jsonContent);

            return result;
        }
    }
}
