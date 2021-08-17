using System.Threading.Tasks;
using Kysect.CentumFramework.Utility;

namespace Greenper.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var mappingResult = await 
                new Mapper(AuthorisationService.Create(
                        "APPLICATION_NAME", 
                        "", 
                        "API_KEY", 
                        "", 
                        new[]{ Scope.DriveReadonly, Scope.SpreadsheetsReadonly }))
                .Map<StudentActivityRow>("1uIdb0zKzFc3MgWafah4XAP1o7Q2dXJK5oGXLXLUoIu8", "Лист1!A2:E11");
        }
    }
}
