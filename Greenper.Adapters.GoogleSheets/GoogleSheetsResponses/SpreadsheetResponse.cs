using System.Collections.Generic;
using Greenper.Adapters.GoogleSheets.Models;

namespace Greenper.Adapters.GoogleSheets.GoogleSheetsResponses
{
    public class SpreadsheetResponse
    {
        public List<GoogleSheetInfo> GoogleSheetInfos { get; }

        public SpreadsheetResponse(List<GoogleSheetInfo> googleSheetInfos)
        {
            GoogleSheetInfos = googleSheetInfos;
        }
    }
}