using System.Collections.Generic;

namespace Greenper.Core.Mapping
{
    public class MappingResult<T>
    {
        public IEnumerable<T> MappedModels { get; set; }

        public MappingResult(IEnumerable<T> mappedModels)
        {
            MappedModels = mappedModels;
        }
    }
}