using OverviewRkiData.Components.Data;
using System.Collections.Generic;

namespace OverviewRkiData.Components.Import
{
    public class ImportResult
    {
        public bool Success => this.DataItems != null;

        public IList<CommonData> DataItems { get; internal set; }
        public string ErrorMessage { get; internal set; }
    }
}