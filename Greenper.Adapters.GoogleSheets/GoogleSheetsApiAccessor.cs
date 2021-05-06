using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Greenper.Adapters.GoogleSheets.Extensions;
using Greenper.Adapters.GoogleSheets.GoogleSheetsResponses;
using Greenper.Adapters.GoogleSheets.Providers;

namespace Greenper.Adapters.GoogleSheets
{
    public class GoogleSheetsApiAccessor : IGoogleSheetsApiAccessor
    {
        public readonly SheetsService SheetsService;

        public GoogleSheetsApiAccessor(SheetsService sheetsService)
        {
            SheetsService = sheetsService;
        }

        public GoogleSheetsApiAccessor() : this(SheetsServiceProvider.GetSheetsService())
        {
        }

        public async Task<SheetResponse> GetSheetAsync(String sheetId, String range)
        {
            SpreadsheetsResource.ValuesResource.GetRequest requestData = SheetsService.Spreadsheets.Values.Get(sheetId, range);
            ValueRange valueRange = await requestData.ExecuteAsync();
            return new SheetResponse(valueRange.Range, valueRange.MajorDimension, valueRange.Values);
        }

        public async Task<SpreadsheetResponse> GetSpreadsheetAsync(String spreadsheetId)
        {
            SpreadsheetsResource.GetRequest requestData = SheetsService.Spreadsheets.Get(spreadsheetId);
            Spreadsheet spreadsheet = await requestData.ExecuteAsync();
            return new SpreadsheetResponse(spreadsheet.SelectSheets().ToList());
        }
    }
}