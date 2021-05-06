using System;

namespace Greenper.Core.Validation
{
    public class ValidatedModel
    {
        public String ColumnIndex { get; }
        public Type ValueType { get; }

        public ValidatedModel(String columnIndex, Type valueType)
        {
            ColumnIndex = columnIndex;
            ValueType = valueType;
        }
    }
}