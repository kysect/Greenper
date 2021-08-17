using System;
using Kysect.Greenper.Attributes;

namespace Kysect.Greenper.Sample
{
    public class StudentActivityRow
    {
        [AssignmentColumn("A")]
        public String Name { get; set; }

        [AssignmentColumns("B:D")]
        public Int32[] Labs { get; set; }
        
        [AssignmentColumn("E")]
        public Int32 Total { get; set; }
    }
}