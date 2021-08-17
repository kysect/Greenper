using System;

namespace Kysect.Greenper.Validation
{
    internal class ValidatedModel
    {
        public String ColumnIndex { get; }
        public Type ValueType { get; }
        public String Name { get; }

        public ValidatedModel(String columnIndex, Type valueType, String name)
        {
            ColumnIndex = columnIndex.ToUpper();
            ValueType = valueType;
            Name = name;
        }
    }
}