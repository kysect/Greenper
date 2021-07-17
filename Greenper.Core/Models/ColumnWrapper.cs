using System;
using Greenper.Core.Extensions;

namespace Greenper.Core.Models
{
    public class ColumnWrapper
    {
        private readonly String _columnName;
        private readonly Int32? _columnNumber;

        public ColumnWrapper(String columnName)
        {
            _columnName = columnName;
        }

        public ColumnWrapper(Int32 columnNumber)
        {
            _columnNumber = columnNumber;
        }

        public Int32 ColumnNumber => _columnName?.IndexOfColumn() ?? throw new ArgumentNullException();
        public String ColumnName => _columnNumber?.NameOfColumn() ?? throw new ArgumentNullException();
    }
}