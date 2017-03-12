using Bibliotheca.Server.Mvc.Middleware.Diagnostics.Exceptions;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class BranchNotFoundException : NotFoundException
    {
        public BranchNotFoundException(string message) : base(message)
        {
        }
    }
}