using System;
using System.Diagnostics.CodeAnalysis;

namespace OverviewRkiData.Components.Data
{
    public class DataItem : IEquatable<DataItem>
    {
        public long ID { get; set; }

        public string Text { get; set; }

        public DateTime SendTime { get; set; }

        public long UserId { get; set; }

        public long ToUserId { get; set; }

        public bool FromMe { get; set; }

        public bool Equals([AllowNull] DataItem other)
        {
            return this.ID.Equals(other.ID);
        }
    }
}
