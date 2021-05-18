using System.Collections.Generic;

namespace Greenper.Core.Mapping
{
    public class MappingResult<T>
    {
        public ICollection<T> MappedModels { get; }

        public MappingResult(ICollection<T> mappedModels)
        {
            MappedModels = mappedModels;
        }
    }
}