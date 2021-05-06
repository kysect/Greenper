using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
