using Bibliotheca.Server.Mvc.Middleware.Diagnostics.Exceptions;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class MkDocsFileNotFoundException : NotFoundException
    {
        public MkDocsFileNotFoundException(string message) : base(message)
        {
        }
    }
}