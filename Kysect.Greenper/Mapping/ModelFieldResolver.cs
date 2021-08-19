using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kysect.Greenper.Extensions;
using Kysect.Greenper.Validation;

namespace Kysect.Greenper.Mapping
{
    internal class ModelFieldResolver
    {
        public IReadOnlyList<ValidatedModel> Models { get; }

        public ModelFieldResolver(IReadOnlyList<ValidatedModel> models)
        {
            Models = models;
        }

        public Object ResolveForSingleValue(IReadOnlyList<Object> row, String range)
        {
            Object cellValue = Models.Single().ColumnIndex.GetValueForCell(range, row);
            var parsedCellValue = ChangeType(cellValue, Models.Single().ValueType);

            return parsedCellValue;
        }

        public ICollection ResolveForManyValues(PropertyInfo property, IReadOnlyList<Object> row, String range) => property.PropertyType.BaseType == typeof(Array)
            ? ResolveForArray(property.PropertyType.GetElementType(), row, range)
            : ResolveForCollection(property.PropertyType, row, range);

        private ICollection ResolveForArray(Type type, IReadOnlyList<Object> row, String range)
        {
            var array = Array.CreateInstance(type, Models.Count);
            for (var modelIndex = 0; modelIndex < Models.Count; modelIndex++)
            {
                Object cellValue = Models[modelIndex].ColumnIndex.GetValueForCell(range, row);
                var parsedCellValue = ChangeType(cellValue, Models[modelIndex].ValueType);

                array.SetValue(parsedCellValue, modelIndex);
            }

            return array;
        }

        private ICollection ResolveForCollection(Type type, IReadOnlyList<Object> row, String range)
        {
            var collection = Activator.CreateInstance(type) ?? 
                             throw new ArgumentException($"Could not create instance of type {type}.");
            var addMethod = collection.GetType().GetMethod("Add", type.GenericTypeArguments) ??
                            throw new InvalidOperationException($"Could not find Add method in collection of type {type}.");

            foreach (var model in Models)
            {
                Object cellValue = model.ColumnIndex.GetValueForCell(range, row);
                var parsedCellValue = ChangeType(cellValue, model.ValueType);
                
                addMethod.Invoke(collection, new Object[] { parsedCellValue });
            }
            
            return (ICollection)collection;
        }

        private Object ChangeType(Object value, Type type)
        {
            Object result;
            
            try
            {
                result = Convert.ChangeType(value, type);
            }
            catch (Exception)
            {
                if (type.IsValueType)
                {
                    result = 0;
                }
                else
                {
                    result = null;
                }
            }

            return result;
        }
    }
}