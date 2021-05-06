using System;

namespace Greenper.Adapters.GoogleSheets.Models
{
    public class GoogleSheetInfo
    {
        public Int32? SheetId { get; }
        public String Title { get; }

        public GoogleSheetInfo(Int32? sheetId, String title)
        {
            SheetId = sheetId;
            Title = title;
        }
    }
}