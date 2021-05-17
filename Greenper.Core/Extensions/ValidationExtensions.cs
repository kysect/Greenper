using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Greenper.Core.Validation;

namespace Greenper.Core.Extensions
{
    public static class ValidationExtensions
    {
        public static Boolean IsValidColumn(this String column)
        {
            if (!column[0].IsValidLetter())
            {
                return false;
            }

            for (var position = 1; position < column.Length; position++)
            {
                if (!Char.IsLetterOrDigit(column[position]))
                {
                    return false;
                }

                if (Char.IsDigit(column[position]))
                {
                    return Int32.TryParse(column.Substring(position), out Int32 value);
                }

                if (!column[position].IsValidLetter())
                {
                    return false;
                }
            }

            return true;
        }

        public static Boolean HasDuplicates(this IEnumerable<ValidatedModel> validatedModels) => 
            validatedModels
                .GroupBy(validatedModel => validatedModel.ColumnIndex)
                .Any(modelGroup => modelGroup.Count() > 1);

        private static Boolean IsValidLetter(this Char letter) =>
            Char.ToUpper(letter) >= 'A' && Char.ToUpper(letter) <= 'Z';

        public static Type GetPropertyElementsType(this PropertyInfo property) => property.PropertyType.IsGenericType
            ? property.PropertyType.GenericTypeArguments[0]
            : property.PropertyType.GetElementType();
    }
}