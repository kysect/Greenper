﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace OopSubmitStatistic.Models
{
    public class GroupRow
    {
        public string Group { get; set; }
        public List<StudentRow> Students { get; set; }

        public GroupRow(string @group, List<StudentRow> students)
        {
            Group = @group;
            Students = students;
        }

        private double Percent<T>(List<T> data, Func<T, bool> predicate)
        {
            return (double) data.Count(predicate) / data.Count() * 100;
        }

        public double AverageSum => Students.Average(s => s.Sum);
        public double AverageExam
        {
            get
            {
                if (!Students.Any(s => s.Exam > 0))
                    return 0;
                return Students.Where(s => s.Exam > 0).Average(s => s.Exam);
            }
        }

        public double Below40Points => Percent(Students, s => s.Sum < 40);
        public double Between40And60 => Percent(Students, s => s.Sum >= 40 && s.Sum < 60);
        public double Pass60Points => Percent(Students, s => s.Sum >= 60);
    }
}