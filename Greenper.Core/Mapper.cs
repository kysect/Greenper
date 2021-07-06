using System;
using System.Collections.Generic;
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
        private readonly IGoogleSheetsApiAccessor _googleSheetsApiAccessor;
        private readonly AssignmentColumnValidator _validator;

        public Mapper(IGoogleSheetsApiAccessor googleSheetsApiAccessor)
        {
            _googleSheetsApiAccessor = googleSheetsApiAccessor;
            _validator = new AssignmentColumnValidator();
        }

        public Mapper() : this(new GoogleSheetsApiAccessor())
        {
        }

        public async Task<MappingResult<T>> Map<T>(String sheetId, String range)
        {
            var sheet = await _googleSheetsApiAccessor.GetSheet(sheetId, range);
            var request = new MappingRequest(sheet, 
                _validator.Validate(new ValidationContext<T>()));

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

                    var fieldResolver = new ModelFieldResolver(validatedModels);

                    Object propertyValue = validatedModels.Count == 1
                        ? fieldResolver.ResolveForSingleValue(row, range)
                        : fieldResolver.ResolveForManyValues(property, row, range);

                    property.SetValue(instance, propertyValue);
                }

                mappedModels.Add(instance);
            }

            return new MappingResult<T>(mappedModels);
        }
    }
}
