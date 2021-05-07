using System;

namespace Greenper.Core.Validation
{
    public class ValidationContext<T>
    {
        public Type Model { get; }

        public ValidationContext()
        {
            Model = typeof(T);
        }
    }
}
