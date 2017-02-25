using System.Threading.Tasks;
using Hangfire.Server;

namespace Bibliotheca.Server.Depository.FileSystem.Jobs
{
    public interface IServiceDiscoveryRegistrationJob
    {
        Task RegisterServiceAsync(PerformContext context);
    }
}