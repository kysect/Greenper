using System;

namespace Greenper.Validation
{
    internal class ValidationContext<T>
    {
        public Type Model { get; }

        public ValidationContext()
        {
            Model = typeof(T);
        }
    }
}
