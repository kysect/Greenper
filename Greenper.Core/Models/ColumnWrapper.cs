using System;

namespace Greenper.Core.Models
{
    internal class ColumnWrapper
    {
        public Int32 ColumnNumber { get; }
        public String ColumnName { get; }

        public ColumnWrapper(String columnName)
        {
            ColumnName = columnName;
            ColumnNumber = GetColumnNumberFromName(columnName);
        }

        public ColumnWrapper(Int32 columnNumber)
        {
            ColumnNumber = columnNumber;
            ColumnName = GetColumnNameFromNumber(columnNumber);
        }

        private Int32 GetColumnNumberFromName(String columnName)
        {
            var result = 0;
            for (var i = columnName.Length - 1; i >= 0; i--)
                result += (Int32)Math.Pow(26, i) * (columnName[i] - 'A' + 1);

            return result;
        }

        private String GetColumnNameFromNumber(Int32 columnNumber)
        {
            Int32 dividend = columnNumber;
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