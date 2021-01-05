using System;
using System.Collections.Generic;
using OopSubmitStatistic.Models;

namespace OopSubmitStatistic
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableParser = TableParser.Create("token");

            List<StudentRow> result = new List<StudentRow>();
            var groupList = new List<string>
            {
                "M3201",
                "M3202",
                "M3203",
                "M3204",
                "M3205",
                "M3206",
                "M3207",
                "M3208",
                "M3209",
                "M3210",
                "M3211",
                "M3212",
            };

            foreach (var group in groupList)
            {
                var googleTableData = new GoogleTableData(
                    "1H75MoSvL-165x5aM-p26eFZcY57UYx0gPtOHhvpGYGw",
                    group,
                    "4",
                    "28",
                    new[] { "A" },
                    "Y");
                var markParser = new MarkParser(googleTableData);
                List<StudentRow> studentSubjectScores = tableParser.Execute(markParser);
                result.AddRange(studentSubjectScores);
            }


        }
    }
}
