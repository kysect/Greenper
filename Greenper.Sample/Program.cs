using System;
using System.Threading.Tasks;
using Greenper.Adapters.GoogleSheets;
using Greenper.Core;

namespace Greenper.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mappingResult = await new Mapper().Map<StudentActivityRow>("1g2NCYFTBjw0CwyyMfl8wliYENStSoC3ws5S7nO7A4OQ", "Лист1!B2:H10");
        }
    }
}
