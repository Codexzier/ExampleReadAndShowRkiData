using System.Collections.Generic;
using System.Linq;

namespace OverviewRkiData.Components.Data
{
    public static class DataHelper
    {
        public static long GetMaxNotExistId<TItem>(IEnumerable<TItem> items) where TItem : class
        {
            if (items.Count() <= 0)
            {
                return 1;
            }

            var maxId = items.Max(m => ((IItem)m).Id) + 1;

            return maxId;
        }
    }
}
