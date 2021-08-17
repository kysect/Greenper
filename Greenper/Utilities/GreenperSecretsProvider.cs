using System.IO;
using Microsoft.Extensions.Configuration;

namespace Greenper.Providers
{
    public class GreenperSecretsProvider
    {
        public static IConfigurationRoot GetGoogleSheetsSecrets()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddUserSecrets<GoogleSheetsSecrets>()
                .Build();
        }

        public static string ApplicationName => 
            GetGoogleSheetsSecrets()
            .GetSection("GoogleSheetsSecrets:ApplicationName")
            .Value;

        public static string ApiKey => 
            GetGoogleSheetsSecrets()
            .GetSection("GoogleSheetsSecrets:ApiKey")
            .Value;
    }
}