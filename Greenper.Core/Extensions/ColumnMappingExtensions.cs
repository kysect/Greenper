using System;
using System.Linq;

namespace Greenper.Core.Extensions
{
    public static class ColumnMappingExtensions
    {
        public static Int32 IndexOfColumn(this String column)
        {
            var rawColumn = new String(column
                .TakeWhile(Char.IsLetter)
                .ToArray());

            var result = 0;
            for (var i = rawColumn.Length - 1; i >= 0; i--)
                result += (Int32)Math.Pow(26, i) * (rawColumn[i] - 'A' + 1);

            return result;
        }

        public static Int32 FirstColumnIndex(this String range)
        {
            var firstColumn = new String(range
                .SkipWhile(c => c != '!')
                .Skip(1)
                .TakeWhile(c => c != ':')
                .ToArray());

            return firstColumn.IndexOfColumn();
        }
    }
}