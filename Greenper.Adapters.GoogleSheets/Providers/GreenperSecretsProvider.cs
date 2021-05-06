using System.IO;
using System.Linq;
using Greenper.Adapters.GoogleSheets.Models;
using Microsoft.Extensions.Configuration;

namespace Greenper.Adapters.GoogleSheets.Providers
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
    }
}