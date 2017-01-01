using Microsoft.AspNetCore.Hosting;

namespace Bibliotheca.Server.Depository.FileSystem.Api.Specs.Infrastructure
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env) : base(env)
        {
            UseServiceDiscovery = false;
        }
    }
}