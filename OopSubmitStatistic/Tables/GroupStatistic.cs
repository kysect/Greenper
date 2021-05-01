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
        public Table Table2;

        public GroupStatistic(List<StudentRow> rows)
        {
            Groups = rows
                .GroupBy(r => r.Group)
                .Select(g => new GroupRow(g.Key, g.ToList()))
                .ToList();

            Table = new Table();
            Table.AddColumn(new TableColumn("[u]Group name[/]"));
            Table.AddColumn(new TableColumn("[u]Average mark[/]"));
            Table.AddColumn(new TableColumn("[u]Average exam mark[/]"));
            Table.AddColumn(new TableColumn("[u]0 - 40, %[/]"));
            Table.AddColumn(new TableColumn("[u]40 - 60, %[/]"));
            Table.AddColumn(new TableColumn("[u]60+, %[/]"));

            Table2 = new Table();
            Table2.AddColumn(new TableColumn("[u]Group name[/]"));
            Table2.AddColumn(new TableColumn("[u]Average mark[/]"));
            Table2.AddColumn(new TableColumn("[u]Average exam mark[/]"));
            Table2.AddColumn(new TableColumn("[u]0 - 40, %[/]"));
            Table2.AddColumn(new TableColumn("[u]40 - 60, %[/]"));
            Table2.AddColumn(new TableColumn("[u]60+, %[/]"));
        }

        public void Generate()
        {
            foreach (GroupRow groupRow in Groups.OrderByDescending(g => g.AverageSum))
            {
                Table.AddRow(
                    groupRow.Group,
                    groupRow.Students.Average(s => s.Sum).ToString("F2"),
                    groupRow.AverageExam.ToString("F2"),
                    (groupRow.Below40Points).ToString("F0"),
                    (groupRow.Between40And60).ToString("F0"),
                    groupRow.Pass60Points.ToString("F0"));
            }

            foreach (GroupRow groupRow in Groups.OrderByDescending(g => g.AverageExam))
            {
                Table2.AddRow(
                    groupRow.Group,
                    groupRow.Students.Average(s => s.Sum).ToString("F2"),
                    groupRow.AverageExam.ToString("F2"),
                    (groupRow.Below40Points).ToString("F0"),
                    (groupRow.Between40And60).ToString("F0"),
                    groupRow.Pass60Points.ToString("F0"));
            }
        }
    }
}