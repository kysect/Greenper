using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Greenper.Core.Attributes;
using Greenper.Core.Extensions;

namespace Greenper.Core.Validation
{
    internal class AssignmentColumnValidator
    {
        public ValidationResult Validate<T>(ValidationContext<T> context)
        {
            var validationResult = ValidateColumn(context)
                .Concat(ValidateColumns(context))
                .ToList();

            if (validationResult.HasDuplicates())
            {
                throw new ArgumentException("Provided model has column index duplicates");
            }

            return new ValidationResult(validationResult);
        }

        private IEnumerable<ValidatedModel> ValidateColumn<T>(ValidationContext<T> context)
        {
            List<PropertyInfo> properties = GetPropertiesWith<AssignmentColumnAttribute, T>(context).ToList();
            foreach (var property in properties)
            {
                var columnAttribute = GetPropertyAttributeOfType<AssignmentColumnAttribute>(property);
                if (!columnAttribute.Column.IsValidColumn())
                {
                    throw new ArgumentException($"Invalid column index: {columnAttribute.Column}");
                }

                yield return new ValidatedModel(columnAttribute.Column, property.PropertyType, property.Name);
            }
        }

        private IEnumerable<ValidatedModel> ValidateColumns<T>(ValidationContext<T> context)
        {
            List<PropertyInfo> properties = GetPropertiesWith<AssignmentColumnsAttribute, T>(context).ToList();
            foreach (var property in properties)
            {
                var columnAttribute = GetPropertyAttributeOfType<AssignmentColumnsAttribute>(property);
                foreach (var column in columnAttribute.Columns)
                {
                    if (!column.IsValidColumn())
                    {
                        throw new ArgumentException($"Invalid column index: {column}");
                    }

                    yield return new ValidatedModel(column, property.GetPropertyElementsType(), property.Name);
                }
            }
        }

        private IEnumerable<PropertyInfo> GetPropertiesWith<TAttribute, T>(ValidationContext<T> context) where TAttribute : Attribute
        {
            return context.Model.GetProperties().Where(property => property.IsDefined(typeof(TAttribute), false));
        }

        private TAttribute GetPropertyAttributeOfType<TAttribute>(MemberInfo property) where TAttribute : Attribute
        {
            return (TAttribute)property.GetCustomAttribute(typeof(TAttribute), false);
        }
    }
}