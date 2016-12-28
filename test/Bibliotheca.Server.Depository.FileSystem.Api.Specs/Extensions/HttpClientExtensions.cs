using System.Net.Http;
using Microsoft.Extensions.Configuration;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs
{
    public static class HttpClientExtensions
    {
        public static void AddSecurityToken(this HttpClient httpClient)
        {
            var contentDirectory = TestContext.GetContentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(contentDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();

            httpClient.DefaultRequestHeaders.Add("Authorization", "SecureToken " + configuration["SecureToken"]);
        }
    }
}