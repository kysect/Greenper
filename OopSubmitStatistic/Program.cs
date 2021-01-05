using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OopSubmitStatistic.Models;
using OopSubmitStatistic.Tables;
using Spectre.Console;

namespace OopSubmitStatistic
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableParser = TableParser.Create("");

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

            var ignores = File.ReadAllLines("ignore.txt");

            result = result
                .Where(s => !ignores.Contains(s.Name))
                .ToList();

            var groupStatistic = new GroupStatistic(result);
            groupStatistic.Groups.Add(new GroupRow("IS avg", result));
            groupStatistic.Generate();
            AnsiConsole.Render(groupStatistic.Table);
        }
    }
}
