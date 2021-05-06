using System;
using System.Threading.Tasks;
using Greenper.Adapters.GoogleSheets.GoogleSheetsResponses;

namespace Greenper.Adapters.GoogleSheets
{
    public interface IGoogleSheetsApiAccessor
    {
        Task<SheetResponse> GetSheet(String sheetId, String range);
        Task<SpreadsheetResponse> GetSpreadsheet(String spreadsheetId);
    }
}