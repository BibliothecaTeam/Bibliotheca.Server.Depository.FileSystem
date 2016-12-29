using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class BranchAlreadyExistsException : Exception
    {
        public BranchAlreadyExistsException(string message) : base(message)
        {
        }
    }
}