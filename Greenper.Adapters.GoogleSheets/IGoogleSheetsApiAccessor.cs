using System;
using System.Threading.Tasks;
using Greenper.Adapters.GoogleSheets.GoogleSheetsResponses;

namespace Greenper.Adapters.GoogleSheets
{
    public interface IGoogleSheetsApiAccessor
    {
        Task<SheetResponse> GetSheetAsync(String sheetId, String range);
        Task<SpreadsheetResponse> GetSpreadsheetAsync(String spreadsheetId);
    }
}