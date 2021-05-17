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

        public Mapper(IGoogleSheetsApiAccessor googleSheetsApiAccessor)
        {
            GoogleSheetsApiAccessor = googleSheetsApiAccessor;
        }

        public async Task<MappingResult<T>> Map<T>(String sheetId, String range)
        {
            var sheet = await GoogleSheetsApiAccessor.GetSheet(sheetId, range);
            var request = new MappingRequest(sheet, 
                new AssignmentColumnValidator().Validate(new ValidationContext<T>()));

            return Map<T>(request);
        }

        private MappingResult<T> Map<T>(MappingRequest request)
        {
            var mappedModels = new List<T>();
            var range = request.Sheet.Range;

            for (var i = 0; i < request.Sheet.Values.Count; i++)
            {
                T instance = Activator.CreateInstance<T>();
                IList<Object> row = request.Sheet.Values[i];
                PropertyInfo[] properties = instance.GetType().GetProperties();

                foreach (var property in properties)
                {
                    if (property is null) throw new NullReferenceException();

                    var validatedModels = request
                        .ValidationResult
                        .ValidatedModels
                        .GetValidatedModelsForProperty(property.Name);

                    var propertyValue = validatedModels.MapToProperty(property, row, range);
                    property.SetValue(instance, propertyValue);
                }

                mappedModels.Add(instance);
            }

            return new MappingResult<T>(mappedModels);
        }
    }
}
