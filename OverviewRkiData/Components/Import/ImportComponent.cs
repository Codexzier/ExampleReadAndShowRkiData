using OverviewRkiData.Components.Data;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace OverviewRkiData.Components.Import
{
    internal class ImportComponent : IImportComponent
    {
        private readonly IDictionary<string, bool> _dvdItemPropertyNames = new Dictionary<string, bool>
        {
            { nameof(CommonData.Title), true },
            { nameof(CommonData.TextContent), true }
        };

        public ImportResult ImportFile(string filename)
        {
            var result = new ImportResult();

            if (string.IsNullOrEmpty(filename))
            {
                result.ErrorMessage = "Filename is empty or null!";
                return result;
            }

            if (!File.Exists(filename))
            {
                result.ErrorMessage = "File not exists!";
                return result;
            }

            var encoding = GetEncoding(filename);
            result.DataItems = new List<CommonData>();

            using (var streamReader = new StreamReader(filename, encoding, true))
            {
                var headers1 = new Dictionary<int, string>();
                string tmp = string.Empty;
                while ((tmp = streamReader.ReadLine()) != null)
                {
                    Debug.WriteLine(tmp);

                    var columns = tmp.Split(';');
                    if (headers1.Count == 0 && this._dvdItemPropertyNames.Keys.Any(a => columns.Any(a2 => a.Contains(a2))))
                    {
                        for (int index = 0; index < columns.Length; index++)
                        {
                            if (!this._dvdItemPropertyNames.ContainsKey(columns[index]))
                            {
                                continue;
                            }

                            headers1.Add(index, columns[index]);
                        }

                        continue;
                    }

                    var reorderColumns = this.ReorderColumns(columns, headers1);
                    var cleanColumns = this.ConcatColumnDescriptionWithAllFollingColumns(columns);

                    var item = new CommonData();
                    var itemType = item.GetType();

                    for (int index = 0; index < cleanColumns.Length; index++)
                    {
                        foreach (var propertyInfo in itemType.GetProperties())
                        {
                            if (!headers1[index].Equals(propertyInfo.Name))
                            {
                                continue;
                            }

                            propertyInfo.SetValue(item, this.CleanupUmlautText(cleanColumns[index]));
                        }
                    }

                    result.DataItems.Add(item);
                }
            }

            return result;
        }

        private string CleanupUmlautText(string cellText)
        {
            return cellText;
        }

        private string[] ReorderColumns(string[] columns, Dictionary<int, string> headers1)
        {
            var reorder = new List<KeyValuePair<int, string>>();

            var keys = headers1.Keys.Select(s => s).ToArray();

            for (int index = 0; index < headers1.Count; index++)
            {
                reorder.Add(new KeyValuePair<int, string>(index, columns[keys[index]]));
            }

            return reorder.OrderBy(b => b.Key).Select(s => s.Value).ToArray();
        }

        private string[] ConcatColumnDescriptionWithAllFollingColumns(string[] columns)
        {
            if (columns.Length == 3)
            {
                return columns;
            }

            var sb = new StringBuilder();
            for (int index = 2; index < columns.Length; index++)
            {
                sb.Append(columns[index] + " ");
            }
            string[] sa = new string[3];
            sa[0] = columns[0];
            sa[1] = columns[1];
            sa[2] = sb.ToString();

            return sa;
        }

        /// <summary>
        /// https://stackoverflow.com/questions/3825390/effective-way-to-find-any-files-encoding
        /// Determines a text file's encoding by analyzing its byte order mark (BOM).
        /// Defaults to ASCII when detection of the text file's endianness fails.
        /// </summary>
        /// <param name="filename">The text file to analyze.</param>
        /// <returns>The detected encoding.</returns>
        public static Encoding GetEncoding(string filename)
        {
            // Read the BOM
            var bom = new byte[4];
            using (var file = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                file.Read(bom, 0, 4);
            }

            var sb = new StringBuilder();
            foreach (var item in bom)
            {
                sb.Append((char)item);
            }

            // Analyze the BOM
            return bom[0] switch
            {
                0x2b when bom[1] == 0x2f && bom[2] == 0x76 => Encoding.UTF7,
                0xef when bom[1] == 0xbb && bom[2] == 0xbf => Encoding.UTF8,
                0xff when bom[1] == 0xfe && bom[2] == 0 && bom[3] == 0 => Encoding.UTF32,
                0xff when bom[1] == 0xfe => Encoding.Unicode,
                0xfe when bom[1] == 0xff => Encoding.BigEndianUnicode,
                0 when bom[1] == 0 && bom[2] == 0xfe && bom[3] == 0xff => new UTF32Encoding(true, true),
                _ => Encoding.ASCII
            };
        }
    }
}
