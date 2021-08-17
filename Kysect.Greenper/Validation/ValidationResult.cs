using System.Collections.Generic;

namespace Kysect.Greenper.Validation
{
    internal class ValidationResult
    {
        public List<ValidatedModel> ValidatedModels { get; }

        public ValidationResult(List<ValidatedModel> validatedModels)
        {
            ValidatedModels = validatedModels;
        }
    }
}