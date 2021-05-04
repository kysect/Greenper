using System;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Greenper.Adapters.GoogleSheets.GoogleSheetsResponses;

namespace Greenper.Adapters.GoogleSheets
{
    public class GoogleSheetsApiAccessor : IGoogleSheetsApiAccessor
    {
        public readonly SheetsService SheetsService;

        public GoogleSheetsApiAccessor(SheetsService sheetsService)
        {
            SheetsService = sheetsService;
        }

        public async Task<SheetResponse> GetSheetAsync(String sheetId, String range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest requestData = SheetsService.Spreadsheets.Values.Get(sheetId, range);
            ValueRange valueRange = await requestData.ExecuteAsync();
            return new SheetResponse(valueRange.Range, valueRange.MajorDimension, valueRange.Values);
        }

        public Task<SpreadsheetResponse> GetSpreadsheetAsync(String spreadsheetId)
        {
            throw new NotImplementedException();
        }
    }
}