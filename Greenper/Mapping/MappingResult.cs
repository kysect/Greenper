using System.Collections.Generic;

namespace Greenper.Mapping
{
    public class MappingResult<T>
    {
        public IReadOnlyCollection<T> MappedModels { get; }

        public MappingResult(IReadOnlyCollection<T> mappedModels)
        {
            MappedModels = mappedModels;
        }
    }
}