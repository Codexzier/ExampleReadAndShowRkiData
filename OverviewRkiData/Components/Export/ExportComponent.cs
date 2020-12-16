using OverviewRkiData.Components.Data;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace OverviewRkiData.Components.Export
{
    internal class ExportComponent : IExportComponent
    {
        public ExportResult ExportToFile(IEnumerable<CommonData> list, string filename)
        {
            var result = new ExportResult();

            if (string.IsNullOrEmpty(filename))
            {
                result.Message = "The filename is null or empty!";
                return result;
            }

            if (File.Exists(filename))
            {
                result.Message = "File exist!";
                return result;
            }

            var sb = new StringBuilder();

            sb.AppendLine($"{nameof(CommonData.Title)};{nameof(CommonData.TextContent)}");
            foreach (var dvdItem in list)
            {
                sb.AppendLine($"{dvdItem.Title};{dvdItem.TextContent}");
            }

            File.WriteAllText(filename, sb.ToString());
            result.Success = sb.Length > 0;
            result.Message = "The data list was export to csv file.";

            return result;
        }
    }
}
