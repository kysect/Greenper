using System;
using System.Collections.Generic;
using System.Linq;
using Google.Apis.Sheets.v4.Data;
using Greenper.Adapters.GoogleSheets.Models;

namespace Greenper.Adapters.GoogleSheets.Extensions
{
    public static class GoogleSheetsAdapterExtensions
    {
        public static IEnumerable<GoogleSheetInfo> SelectSheets(this Spreadsheet spreadsheet) =>
            spreadsheet.Sheets
                .Select(sheet => sheet.Properties)
                .Select(sheetProperty => new GoogleSheetInfo(sheetProperty.SheetId, sheetProperty.Title));
    }
}