using System;

namespace Greenper.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AssignmentColumnAttribute : Attribute
    {
        public String Column { get; }

        public AssignmentColumnAttribute(String column)
        {
            Column = column;
        }
    }
}
