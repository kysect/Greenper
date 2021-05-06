using System;
using System.Collections.Generic;

namespace Greenper.Adapters.GoogleSheets.GoogleSheetsResponses
{
    public class SheetResponse
    {
        public string Range { get; }
        public string MajorDimension { get; }
        public IList<IList<object>> Values { get; }

        public SheetResponse(String range, String majorDimension, IList<IList<Object>> values)
        {
            Range = range;
            MajorDimension = majorDimension;
            Values = values;
        }
    }
}