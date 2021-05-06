using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Greenper.Attributes;
using Greenper.Core.Extensions;

namespace Greenper.Core.Validation
{
    public class AssignmentColumnValidator<T>
    {
        public ValidationContext<T> Context { get; }

        public AssignmentColumnValidator(ValidationContext<T> context)
        {
            Context = context;
        }

        public ValidationResult Validate()
        {
            var validationResult = ValidateColumn()
                .Concat(ValidateColumns())
                .ToList();

            if (validationResult.HasDuplicates())
            {
                throw new ArgumentException("Provided model has column index duplicates");
            }

            return new ValidationResult(validationResult);
        }

        private IEnumerable<ValidatedModel> ValidateColumn()
        {
            var properties = GetPropertiesWith<AssignmentColumnAttribute>().ToList();
            foreach (var property in properties)
            {
                var columnAttribute = GetPropertyAttributeOfType<AssignmentColumnAttribute>(property);
                if (!columnAttribute.Column.IsValidColumn())
                {
                    throw new ArgumentException($"Invalid column index: {columnAttribute.Column}");
                }

                yield return new ValidatedModel(columnAttribute.Column, property.PropertyType);
            }
        }

        private IEnumerable<ValidatedModel> ValidateColumns()
        {
            var properties = GetPropertiesWith<AssignmentColumnsAttribute>().ToList();
            foreach (var property in properties)
            {
                var columnAttribute = GetPropertyAttributeOfType<AssignmentColumnsAttribute>(property);
                foreach (var column in columnAttribute.Columns)
                {
                    if (!column.IsValidColumn())
                    {
                        throw new ArgumentException($"Invalid column index: {column}");
                    }

                    yield return new ValidatedModel(column, property.PropertyType.GetElementType());
                }
            }
        }

        private IEnumerable<PropertyInfo> GetPropertiesWith<TAttribute>() where TAttribute : Attribute
        {
            return Context.Model.GetProperties().Where(property => property.IsDefined(typeof(TAttribute), false));
        }

        private TAttribute GetPropertyAttributeOfType<TAttribute>(MemberInfo property) where TAttribute : Attribute
        {
            return (TAttribute) property.GetCustomAttribute(typeof(TAttribute), false);
        }
    }
}