using System;
using System.Collections.Generic;
using Greenper.Validation;

namespace Greenper.Mapping
{
    internal class MappingRequest
    {
        public String DataRange { get; }
        public IReadOnlyList<IReadOnlyList<Object>> Data { get; }
        
        public ValidationResult ValidationResult { get; }

        public MappingRequest(
            string dataRange, 
            IReadOnlyList<IReadOnlyList<object>> data, 
            ValidationResult validationResult)
        {
            DataRange = dataRange;
            Data = data;
            ValidationResult = validationResult;
        }
    }
}