using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kysect.CentumFramework.Utility;
using Kysect.Greenper;
using OopSubmitStatistic.Models;
using OopSubmitStatistic.Tables;
using Spectre.Console;

namespace OopSubmitStatistic
{
    class Program
    {
        static async Task Main(string[] args)
        {
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

            var authorisationService = AuthorisationService.Create(
                "APPLICATION_NAME",
                "",
                "API_KEY",
                "",
                new[] {Scope.DriveReadonly, Scope.SpreadsheetsReadonly});

            var mapper = new Mapper(authorisationService);
            
            foreach (var group in groupList)
            {
                var mappingResult = await mapper.Map<StudentRow>("1H75MoSvL-165x5aM-p26eFZcY57UYx0gPtOHhvpGYGw", group + "!A4:Y28");

                foreach (var mappedModel in mappingResult.MappedModels)
                {
                    mappedModel.Group = group;
                }
                
                result.AddRange(mappingResult.MappedModels);
            }

            var ignores = await File.ReadAllLinesAsync("ignore.txt");

            result = result
                .Where(s => !ignores.Contains(s.Name))
                .ToList();

            var groupStatistic = new GroupStatistic(result);
            groupStatistic.Groups.Add(new GroupRow("IS avg", result));
            groupStatistic.Generate();
            AnsiConsole.Render(groupStatistic.Table);
            AnsiConsole.Render(groupStatistic.Table2);
        }
    }
}
