using OverviewRkiData.Components.Data;
using System.Collections.Generic;

namespace OverviewRkiData.Components.Export
{
    public interface IExportComponent
    {
        ExportResult ExportToFile(IEnumerable<CommonData> list, string exportFilename);
    }
}