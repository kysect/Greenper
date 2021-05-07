﻿using System;
using Greenper.Attributes;

namespace Greenper.Sample
{
    public class StudentActivityRow
    {
        [AssignmentColumn("B")]
        public String UniversitySystemId { get; set; }

        [AssignmentColumn("C")]
        public String Name { get; set; }

        [AssignmentColumns(new []{"D", "E", "F"})]
        public Int32[] Labs { get; set; }

        [AssignmentColumn("G")]
        public Int32 Exam { get; set; }

        [AssignmentColumn("H")]
        public Int32 Total { get; set; }
    }
}