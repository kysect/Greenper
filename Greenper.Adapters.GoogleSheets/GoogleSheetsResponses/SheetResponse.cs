using System;
using System.Collections.Generic;

namespace Greenper.Adapters.GoogleSheets.GoogleSheetsResponses
{
    public class SheetResponse
    {
        public string Range { get; }
        public string MajorDimension { get; }
        public IList<IList<object>> Values { get; }

        public SheetResponse(String majorDimension, String range, IList<IList<Object>> values)
        {
            MajorDimension = majorDimension;
            Range = range;
            Values = values;
        }
    }
}