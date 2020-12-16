using OverviewRkiData.Components.Data;
using System.Collections.Generic;

namespace OverviewRkiData.Components.LocalStorage
{
    public interface ILocalStorageComponent
    {
        IReadOnlyCollection<CommonData> DVDs { get; }
        void Add(CommonData dvdItem);
        void Remove(CommonData dvdItem);
        bool Save();
    }
}
