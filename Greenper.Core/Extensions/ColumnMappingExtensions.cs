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

        public static String NameOfColumn(this Int32 columnIndex)
        {
            Int32 dividend = columnIndex;
            String result = String.Empty;

            while (dividend > 0)
            {
                Int32 modulo = (dividend - 1) % 26;
                result += Convert.ToChar('A' + modulo);
                dividend = (dividend - modulo) / 26;
            }

            return result;
        }
    }
}