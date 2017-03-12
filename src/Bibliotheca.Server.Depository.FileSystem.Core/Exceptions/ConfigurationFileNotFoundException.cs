using Bibliotheca.Server.Mvc.Middleware.Diagnostics.Exceptions;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class ConfigurationFileNotFoundException : NotFoundException
    {
        public ConfigurationFileNotFoundException(string message) : base(message)
        {
        }
    }
}