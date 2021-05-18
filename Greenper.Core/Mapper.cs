using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Greenper.Adapters.GoogleSheets;
using Greenper.Core.Extensions;
using Greenper.Core.Mapping;
using Greenper.Core.Validation;

namespace Greenper.Core
{
    public class Mapper
    {
        public IGoogleSheetsApiAccessor GoogleSheetsApiAccessor { get; }
        public AssignmentColumnValidator Validator { get; }

        public Mapper(IGoogleSheetsApiAccessor googleSheetsApiAccessor)
        {
            GoogleSheetsApiAccessor = googleSheetsApiAccessor;
            Validator = new AssignmentColumnValidator();
        }

        public Mapper() : this(new GoogleSheetsApiAccessor())
        {
        }

        public async Task<MappingResult<T>> Map<T>(String sheetId, String range)
        {
            var sheet = await GoogleSheetsApiAccessor.GetSheet(sheetId, range);
            var request = new MappingRequest(sheet, 
                Validator.Validate(new ValidationContext<T>()));

            return Map<T>(request);
        }

        private MappingResult<T> Map<T>(MappingRequest request)
        {
            var mappedModels = new List<T>();
            var range = request.Sheet.Range;
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var row in request.Sheet.Values)
            {
                T instance = Activator.CreateInstance<T>();

                foreach (var property in properties)
                {
                    var validatedModels = request
                        .ValidationResult
                        .ValidatedModels
                        .GetValidatedModelsForProperty(property.Name);

                    Object propertyValue = validatedModels.MapToProperty(property, row, range);
                    property.SetValue(instance, propertyValue);
                }

                mappedModels.Add(instance);
            }

            return new MappingResult<T>(mappedModels);
        }
    }
}
