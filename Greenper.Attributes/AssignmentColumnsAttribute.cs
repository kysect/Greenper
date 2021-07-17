using System;

namespace Greenper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AssignmentColumnsAttribute : Attribute
    {
        public String[] Columns { get; }

        public AssignmentColumnsAttribute(String[] columns)
        {
            Columns = columns;
        }

        public AssignmentColumnsAttribute(String columnRange)
        {
            Columns = GetColumnsFromRange(columnRange);
        }

        private String[] GetColumnsFromRange(String columnRange)
        {
            String[] range = columnRange.Split(":");

            Int32 firstIndex = GetColumnIndexFromName(range[0]);
            Int32 lastIndex = GetColumnIndexFromName(range[1]);

            Int32 count = lastIndex - firstIndex + 1;
            String[] columns = new String[count];

            for (Int32 i = firstIndex; i < lastIndex + 1; i++)
            {
                columns[i - firstIndex] = GetColumnNameFromIndex(i);
            }

            return columns;
        }

        private Int32 GetColumnIndexFromName(String columnName)
        {
            Char[] characters = columnName.ToUpperInvariant().ToCharArray();
            Int32 sum = 0;

            for (Int32 i = 0; i < characters.Length; i++)
            {
                sum *= 26;
                sum += (characters[i] - 'A' + 1);
            }

            return sum;
        }

        private String GetColumnNameFromIndex(Int32 columnNumber)
        {
            Int32 dividend = columnNumber;
            String columnName = String.Empty;

            while (dividend > 0)
            {
                Int32 modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }
    }
}