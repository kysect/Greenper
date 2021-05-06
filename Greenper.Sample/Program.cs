using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4.Data;
using Greenper.Adapters.GoogleSheets;

namespace Greenper.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            GoogleSheetsApiAccessor accessor = new GoogleSheetsApiAccessor();
            var spreadsheet = await accessor.GetSpreadsheet("1H75MoSvL-165x5aM-p26eFZcY57UYx0gPtOHhvpGYGw");
            foreach (var googleSheetInfo in spreadsheet.GoogleSheetInfos)
            {
                var sheet = await accessor.GetSheet("1H75MoSvL-165x5aM-p26eFZcY57UYx0gPtOHhvpGYGw", $"{googleSheetInfo.Title}!A1:Y26");
            }
        }
    }
}
