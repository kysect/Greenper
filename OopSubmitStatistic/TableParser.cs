using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace OopSubmitStatistic
{
    public class TableParser
    {
        private readonly SheetsService _service;

        public TableParser(SheetsService service)
        {
            _service = service;
        }

        public static TableParser Create(string serviceToken)
        {
            return new TableParser(GetServiceForApiToken(serviceToken));
        }

        public T Execute<T>(ITableRequest<T> request)
        {
            SpreadsheetsResource.ValuesResource.GetRequest dataRequest = _service.Spreadsheets.Values.Get(request.Id, request.Range);
            ValueRange data = dataRequest.Execute();
            return request.Parse(data);
        }

        //private static SheetsService GetServiceForCredential(string serviceToken)
        //{
        //    GoogleCredential credential = GoogleCredential
        //        .FromJson(serviceToken)
        //        .CreateScoped(SheetsService.Scope.SpreadsheetsReadonly);

        //    return new SheetsService(new BaseClientService.Initializer
        //    {
        //        ApplicationName = "IwentysTableParser",
        //        HttpClientInitializer = credential
        //    });
        //}

        private static SheetsService GetServiceForApiToken(string serviceToken)
        {
            return new SheetsService(new BaseClientService.Initializer
            {
                ApplicationName = "IwentysTableParser",
                ApiKey = serviceToken
            });
        }
    }
}