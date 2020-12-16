using OverviewRkiData.Components.Data;

namespace OverviewRkiData.Components.LocalStorage
{
    public class LocalStorageResult
    {
        public LocalStorageResult(CommonData item, int countExist)
        {
            this.Item = item;
            this.CountExist = countExist;
        }

        public CommonData Item { get; }
        public int CountExist { get; }
    }
}
