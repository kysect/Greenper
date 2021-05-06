using System;

namespace Greenper.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AssignmentColumnsAttribute : Attribute
    {
        public String[] Columns { get; }

        public AssignmentColumnsAttribute(String[] columns)
        {
            Columns = columns;
        }
    }
}