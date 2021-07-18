using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Greenper.Core.Models;
using Greenper.Core.Validation;

namespace Greenper.Core.Extensions
{
    public static class MappingExtensions
    {
        public static List<ValidatedModel> GetValidatedModelsForProperty(this List<ValidatedModel> validatedModels,
            String propertyName) =>
            validatedModels.Where(model => model.Name == propertyName)
                .ToList();

        internal static Object GetValueForCell(this String column, String range, IList<Object> row)
        {
            var columnIndex = new ColumnIndex(column);
            var indexRange = new IndexRange(range);
            var currentColumn = new ColumnWrapper(columnIndex.ColumnName);
            var firstColumn = new ColumnWrapper(indexRange.FirstColumnName);

            return row[currentColumn.ColumnNumber - firstColumn.ColumnNumber];
        }
    }
}