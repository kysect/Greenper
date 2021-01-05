using System;
using System.Collections.Generic;

namespace OopSubmitStatistic
{
    class Program
    {
        static void Main(string[] args)
        {
            var tableParser = TableParser.Create("token");

            var googleTableData = new GoogleTableData(
                "1H75MoSvL-165x5aM-p26eFZcY57UYx0gPtOHhvpGYGw",
                "M3201",
                "4",
                "24",
                new []{"A"},
                "Y");
            var markParser = new MarkParser(googleTableData);
            List<StudentSubjectScore> studentSubjectScores = tableParser.Execute(markParser);
        }
    }
}
