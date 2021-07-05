using System;
using System.Linq;

namespace Greenper.Core.Models
{
    public class ColumnIndex
    {
        public String RawColumn { get; }

        public ColumnIndex(String rawColumn)
        {
            RawColumn = rawColumn;
        }

        public String ColumnName => new String(RawColumn.TakeWhile(Char.IsLetter).ToArray());
    }
}