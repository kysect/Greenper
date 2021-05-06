using System;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

namespace Greenper.Adapters.GoogleSheets.Providers
{
    public class SheetsServiceProvider
    {
        private static SheetsService _sheetsService;

        public static SheetsService GetSheetsService() => _sheetsService ??=
            new SheetsService(new BaseClientService.Initializer()
                { ApplicationName = ApplicationName, ApiKey = ApiKey });

        private static string ApplicationName => GreenperSecretsProvider
            .GetGoogleSheetsSecrets()
            .GetSection("GoogleSheetsSecrets:ApplicationName")
            .Value;

        private static string ApiKey => GreenperSecretsProvider
            .GetGoogleSheetsSecrets()
            .GetSection("GoogleSheetsSecrets:ApiKey")
            .Value;
    }
}