using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Kysect.Greenper.Extensions;
using Kysect.CentumFramework.Drive;
using Kysect.CentumFramework.Drive.Entities;
using Kysect.CentumFramework.Drive.Extensions;
using Kysect.CentumFramework.Sheets;
using Kysect.CentumFramework.Sheets.Entities;
using Kysect.CentumFramework.Sheets.Extensions;
using Kysect.CentumFramework.Sheets.Models.Indices;
using Kysect.CentumFramework.Utility;
using Kysect.Greenper.Mapping;
using Kysect.Greenper.Models;
using Kysect.Greenper.Providers;
using Kysect.Greenper.Validation;

namespace Kysect.Greenper
{
    public class Mapper
    {
        private readonly DriveService _driveService;
        private readonly SheetsService _sheetsService;
        
        private readonly AssignmentColumnValidator _validator;

        public Mapper(AuthorisationService authorisationService)
        {
            _driveService = new DriveService(authorisationService);
            _sheetsService = new SheetsService(authorisationService);
            
            _validator = new AssignmentColumnValidator();
        }

        public Mapper() 
            : this(AuthorisationService.Create(
                GreenperSecretsProvider.ApplicationName, 
                String.Empty, 
                GreenperSecretsProvider.ApiKey, 
                String.Empty, 
                new List<Scope>()))
        {
        }

        public async Task<MappingResult<T>> Map<T>(String spreadSheetId, String range)
        {
            File spreadsheetFile = await _driveService.GetFileAsync(spreadSheetId);
            Spreadsheet spreadsheet = await _sheetsService.GetSpreadsheetAsync(spreadsheetFile);

            var indexRange = new IndexRange(range);
            Sheet sheet = spreadsheet.Sheets.Single(s => s.Name == indexRange.SheetName);
            
            IReadOnlyList<IReadOnlyList<Object>> data = await sheet
                .GetValuesAsync(new SheetIndexRange(indexRange.ColumnIndexRange), CancellationToken.None);

            var request = new MappingRequest(range, data, 
                _validator.Validate(new ValidationContext<T>()));

            return Map<T>(request);
        }

        private MappingResult<T> Map<T>(MappingRequest request)
        {
            var mappedModels = new List<T>();
            var range = request.DataRange;
            PropertyInfo[] properties = typeof(T).GetProperties();

            foreach (var row in request.Data)
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
