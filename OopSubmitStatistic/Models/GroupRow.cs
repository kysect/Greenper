using System;
using System.Collections.Generic;
using System.Linq;

namespace OopSubmitStatistic.Models
{
    public class GroupRow
    {
        public string Group { get; }
        public List<StudentRow> Students { get; }

        public GroupRow(string @group, List<StudentRow> students)
        {
            Group = @group;
            Students = students;
        }

        private double Percent<T>(List<T> data, Func<T, bool> predicate)
        {
            return (double) data.Count(predicate) / data.Count() * 100;
        }

        public double AverageTotal => Students.Average(s => s.Total);
        public double AverageExam
        {
            get
            {
                if (!Students.Any(s => s.Exam > 0))
                    return 0;
                return Students.Where(s => s.Exam > 0).Average(s => s.Exam);
            }
        }

        public double Below40Points => Percent(Students, s => s.Total < 40);
        public double Between40And60 => Percent(Students, s => s.Total >= 40 && s.Total < 60);
        public double Pass60Points => Percent(Students, s => s.Total >= 60);
    }
}