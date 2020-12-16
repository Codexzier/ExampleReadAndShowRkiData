using System;
using System.Collections.Generic;
using System.Text;

namespace OverviewRkiData.Components.Data
{
    public class CommonData
    {
        public long ID { get; set; }
        public string Title { get; internal set; }
        public string TextContent { get; internal set; }
    }
}
