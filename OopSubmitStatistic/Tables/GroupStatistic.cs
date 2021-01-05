using System.Collections.Generic;
using System.Linq;
using OopSubmitStatistic.Models;
using Spectre.Console;

namespace OopSubmitStatistic.Tables
{
    public class GroupStatistic
    {
        public List<GroupRow> Groups;

        public Table Table;

        public GroupStatistic(List<StudentRow> rows)
        {
            Groups = rows
                .GroupBy(r => r.Group)
                .Select(g => new GroupRow(g.Key, g.ToList()))
                .ToList();

            Table = new Table();
            Table.AddColumn(new TableColumn("[u]Group name[/]"));
            Table.AddColumn(new TableColumn("[u]Average mark[/]"));
            Table.AddColumn(new TableColumn("[u]>= 40, %[/]"));
            Table.AddColumn(new TableColumn("[u]>= 60, %[/]"));
        }

        public void Generate()
        {
            foreach (GroupRow groupRow in Groups.OrderByDescending(g => g.AverageSum))
            {
                Table.AddRow(
                    groupRow.Group,
                    groupRow.Students.Average(s => s.Sum).ToString("F2"),
                    groupRow.Pass40Points.ToString("F0"),
                    groupRow.Pass60Points.ToString("F0"));
            }
        }
    }
}