using System;
using Kysect.Greenper.Models;

namespace Kysect.Greenper.Attributes
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

            Int32 firstIndex = new ColumnWrapper(range[0]).ColumnNumber;
            Int32 lastIndex = new ColumnWrapper(range[1]).ColumnNumber;

            Int32 count = lastIndex - firstIndex + 1;
            String[] columns = new String[count];

            for (Int32 index = firstIndex; index < lastIndex + 1; index++)
            {
                var columnWrapper = new ColumnWrapper(index);
                columns[index - firstIndex] = columnWrapper.ColumnName;
            }

            return columns;
        }
    }
}