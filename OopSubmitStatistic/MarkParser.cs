using System.Collections.Generic;
using Google.Apis.Sheets.v4.Data;
using OopSubmitStatistic.Models;

namespace OopSubmitStatistic
{
    public class MarkParser : ITableRequest<List<StudentRow>>
    {
        private readonly TableStringHelper _helper;


        public MarkParser(GoogleTableData tableData)
        {
            _helper = new TableStringHelper(tableData);
        }

        public string Id => _helper.Id;
        public string Range => _helper.Range;

        public List<StudentRow> Parse(ValueRange values)
        {
            var result = new List<StudentRow>();
            foreach (IList<object> row in values.Values)
            {
                if (StudentRow.Parse(row, out var student))
                {
                    result.Add(student);
                }
            }

            return result;
        }
    }
}