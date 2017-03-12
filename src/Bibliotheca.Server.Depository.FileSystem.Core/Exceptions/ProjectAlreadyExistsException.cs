using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class ProjectAlreadyExistsException : Exception
    {
        public ProjectAlreadyExistsException(string message) : base(message)
        {
        }
    }
}