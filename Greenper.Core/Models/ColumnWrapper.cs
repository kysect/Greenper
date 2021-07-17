using System;
using Greenper.Core.Extensions;

namespace Greenper.Core.Models
{
    public class ColumnWrapper
    {
        private readonly String _columnName;
        private readonly Int32 _columnNumber;

        public ColumnWrapper(String columnName)
        {
            _columnName = columnName;
        }

        public ColumnWrapper(Int32 columnNumber)
        {
            _columnNumber = columnNumber;
        }

        public Int32 ColumnNumber => GetColumnNumberFromName();
        public String ColumnName => GetColumnNameFromNumber();

        private Int32 GetColumnNumberFromName()
        {
            var result = 0;
            for (var i = _columnName.Length - 1; i >= 0; i--)
                result += (Int32)Math.Pow(26, i) * (_columnName[i] - 'A' + 1);

            return result;
        }

        private String GetColumnNameFromNumber()
        {
            Int32 dividend = _columnNumber;
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