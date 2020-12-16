using OverviewRkiData.Components.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace OverviewRkiData.Components.LocalStorage
{
    internal class LocalStorageComponent : ILocalStorageComponent
    {
        private readonly string _storageFile = $"{Environment.CurrentDirectory}/localStorage.json";
        private readonly IList<CommonData> _items;

        public LocalStorageComponent()
        {
            if (!File.Exists(this._storageFile))
            {
                this._items = new List<CommonData>();
                return;
            }

            var jsonToRead = File.ReadAllText(this._storageFile);
            this._items = JsonConvert.DeserializeObject<List<CommonData>>(jsonToRead);
        }

        public IReadOnlyCollection<CommonData> DVDs => (IReadOnlyCollection<CommonData>)this._items;

        public void Add(CommonData dvdItem)
        {
            this._items.Add(dvdItem);
        }

        public void Remove(CommonData dvdItem)
        {
            this._items.Remove(dvdItem);
        }

        public bool Save()
        {
            var jsonToSave = JsonConvert.SerializeObject(this._items);
            File.WriteAllText(this._storageFile, jsonToSave);
            return true;
        }
    }
}
