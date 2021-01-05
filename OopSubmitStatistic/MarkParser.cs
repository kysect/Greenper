using System.Collections.Generic;
using System.Linq;
using Google.Apis.Logging;
using Google.Apis.Sheets.v4.Data;

namespace OopSubmitStatistic
{
    public class MarkParser : ITableRequest<List<StudentSubjectScore>>
    {
        private readonly TableStringHelper _helper;


        public MarkParser(GoogleTableData tableData)
        {
            _helper = new TableStringHelper(tableData);
        }

        public string Id => _helper.Id;
        public string Range => _helper.Range;

        public List<StudentSubjectScore> Parse(ValueRange values)
        {
            var result = new List<StudentSubjectScore>();
            foreach (IList<object> row in values.Values)
            {
                object name = row[_helper.NameColumnNum];
                object score = row[_helper.ScoreColumnNum];
                if (name is not null && score is not null)
                {
                    string fullName = string.Join(" ", _helper.NameColumns.Select(c => row[c]));

                    result.Add(new StudentSubjectScore(fullName,
                        score.ToString()));
                }
                else
                {
                }
            }

            return result;
        }
    }
}