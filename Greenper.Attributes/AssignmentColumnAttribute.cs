using System;

namespace Greenper.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AssignmentColumnAttribute : Attribute
    {
        public String Column { get; }

        public AssignmentColumnAttribute(String column)
        {
            Column = column;
        }
    }
}
