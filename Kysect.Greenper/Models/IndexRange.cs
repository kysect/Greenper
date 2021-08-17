using System;
using Kysect.Greenper.Extensions;

namespace Kysect.Greenper.Models
{
    internal class IndexRange
    {
        public String RawRange { get; }

        private const Char SheetRangeDelimiter = '!';
        private const Char ColumnRangeDelimiter = ':';

        public IndexRange(String rawRange)
        {
            RawRange = rawRange;
        }

        public String SheetName => RawRange.TakeBefore(SheetRangeDelimiter);
        public String ColumnIndexRange => RawRange.TakeAfter(SheetRangeDelimiter);
        public String FirstColumnName => ColumnIndexRange.TakeBefore(ColumnRangeDelimiter, Char.IsLetter);
        public String LastColumnName => ColumnIndexRange.TakeAfter(ColumnRangeDelimiter, Char.IsLetter);
    }
}