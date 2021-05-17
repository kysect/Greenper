using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Greenper.Core.Mapping;
using Greenper.Core.Validation;

namespace Greenper.Core.Extensions
{
    public static class MappingExtensions
    {
        public static List<ValidatedModel> GetValidatedModelsForProperty(this List<ValidatedModel> validatedModels,
            String propertyName) =>
            validatedModels.Where(model => model.Name == propertyName)
                .ToList();

        public static Object MapToProperty(this List<ValidatedModel> models, PropertyInfo property, IList<Object> row, String range) =>
            models.Count == 1
                ? models.Single().MapToSingleValue(property, row, range)
                : models.MapToValueCollection(property, row, range);

        private static Object MapToSingleValue(this ValidatedModel model, PropertyInfo property, IList<Object> row, String range)
        {
            Object cellValue = model.ColumnIndex.GetValueForCell(range, row);
            var parsedCellValue = Convert.ChangeType(cellValue, model.ValueType);

            return parsedCellValue;
        }

        private static Object MapToValueCollection(this List<ValidatedModel> models, PropertyInfo property, IList<Object> row, String range) =>
            property.PropertyType.BaseType == typeof(Array)
                ? models.MapToArray(property.PropertyType.GetElementType(), row, range)
                : models.MapToCollection(property.PropertyType, row, range);

        private static Array MapToArray(this List<ValidatedModel> models, Type type, IList<Object> row, String range)
        {
            var array = Array.CreateInstance(type, models.Count);
            for (var modelIndex = 0; modelIndex < models.Count; modelIndex++)
            {
                Object cellValue = models[modelIndex].ColumnIndex.GetValueForCell(range, row);
                var parsedCellValue = Convert.ChangeType(cellValue, models[modelIndex].ValueType);

                array.SetValue(parsedCellValue, modelIndex);
            }

            return array;
        }

        private static ICollection MapToCollection(this List<ValidatedModel> models, Type type, IList<Object> row,
            String range)
        {
            var collection = Activator.CreateInstance(type) ?? throw new Exception($"Could not create instance of type {type}.");
            var addMethod = collection
                .GetType()
                .GetMethods()
                .FirstOrDefault(method => method.Name == "Add" && method.GetParameters().Count<ParameterInfo>() == 1) ?? throw new Exception();

            for (var i = 0; i < models.Count; i++)
            {
                Object cellValue = models[i].ColumnIndex.GetValueForCell(range, row);
                var parsedCellValue = Convert.ChangeType(cellValue, models[i].ValueType);

                addMethod.Invoke(collection, new Object[] { parsedCellValue });
            }

            return (ICollection)collection;
        }

        private static Object GetValueForCell(this String column, String range, IList<Object> values)
        {
            return values[column.IndexOfColumn() - range.FirstColumnIndex()];
        }
    }
}