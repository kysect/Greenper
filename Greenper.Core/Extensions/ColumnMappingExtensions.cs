using System;

namespace Greenper.Core.Extensions
{
    public static class ColumnMappingExtensions
    {
        public static Int32 IndexOfColumn(this String column)
        {
            var result = 0;
            for (var i = column.Length - 1; i >= 0; i--)
                result += (Int32)Math.Pow(26, i) * (column[i] - 'A' + 1);

            return result;
        }
    }
}