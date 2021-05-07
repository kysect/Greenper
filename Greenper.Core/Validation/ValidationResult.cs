using System.Collections.Generic;

namespace Greenper.Core.Validation
{
    public class ValidationResult
    {
        public List<ValidatedModel> ValidatedModels { get; }

        public ValidationResult(List<ValidatedModel> validatedModels)
        {
            ValidatedModels = validatedModels;
        }
    }
}