using Greenper.Adapters.GoogleSheets.GoogleSheetsResponses;
using Greenper.Core.Validation;

namespace Greenper.Core.Mapping
{
    public class MappingRequest
    {
        public SheetResponse Sheet { get; }
        public ValidationResult ValidationResult { get; }

        public MappingRequest(SheetResponse sheet, ValidationResult validationResult)
        {
            Sheet = sheet;
            ValidationResult = validationResult;
        }
    }
}