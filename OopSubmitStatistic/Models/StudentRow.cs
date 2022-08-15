using System.Collections.Generic;
using Kysect.Greenper.Attributes;

namespace OopSubmitStatistic.Models
{
    public class StudentRow
    {
        public string Group { get; set; }
        
        [AssignmentColumn("A")]
        public string Name { get; set; }
        
        [AssignmentColumns(new []{ "E", "H", "K", "N", "Q", "T" })]
        public double[] Labs { get; set; }
        
        [AssignmentColumn("U")]
        public double Theory { get; set; }
        
        [AssignmentColumns(new []{ "V", "W" })]
        public double Lk { get; set; }
        
        [AssignmentColumn("X")]
        public double Exam { get; set; }
        
        [AssignmentColumn("Y")]
        public double Total { get; set; }
    }
}