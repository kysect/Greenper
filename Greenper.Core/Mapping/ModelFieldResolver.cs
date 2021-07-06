using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Greenper.Core.Extensions;
using Greenper.Core.Models;
using Greenper.Core.Validation;

namespace Greenper.Core.Mapping
{
    public class ModelFieldResolver
    {
        public List<ValidatedModel> Models { get; }

        public ModelFieldResolver(List<ValidatedModel> models)
        {
            Models = models;
        }

        public Object ResolveForSingleValue(IList<Object> row, String range)
        {
            Object cellValue = Models.Single().ColumnIndex.GetValueForCell(range, row);
            var parsedCellValue = Convert.ChangeType(cellValue, Models.Single().ValueType);

            return parsedCellValue;
        }

        public ICollection ResolveForManyValues(PropertyInfo property, IList<Object> row, String range) => property.PropertyType.BaseType == typeof(Array)
            ? ResolveForArray(property.PropertyType.GetElementType(), row, range)
            : ResolveForCollection(property.PropertyType, row, range);

        private ICollection ResolveForArray(Type type, IList<Object> row, String range)
        {
            var array = Array.CreateInstance(type, Models.Count);
            for (var modelIndex = 0; modelIndex < Models.Count; modelIndex++)
            {
                Object cellValue = Models[modelIndex].ColumnIndex.GetValueForCell(range, row);
                var parsedCellValue = Convert.ChangeType(cellValue, Models[modelIndex].ValueType);

                array.SetValue(parsedCellValue, modelIndex);
            }

            return array;
        }

        private ICollection ResolveForCollection(Type type, IList<Object> row, String range)
        {
            var collection = Activator.CreateInstance(type) ?? 
                             throw new ArgumentException($"Could not create instance of type {type}.");
            var addMethod = collection.GetType().GetMethod("Add", type.GenericTypeArguments) ??
                            throw new InvalidOperationException($"Could not find Add method in collection of type {type}.");

            foreach (var model in Models)
            {
                Object cellValue = model.ColumnIndex.GetValueForCell(range, row);
                var parsedCellValue = Convert.ChangeType(cellValue, model.ValueType);

                addMethod.Invoke(collection, new Object[] { parsedCellValue });
            }
            
            return (ICollection)collection;
        }
    }
}