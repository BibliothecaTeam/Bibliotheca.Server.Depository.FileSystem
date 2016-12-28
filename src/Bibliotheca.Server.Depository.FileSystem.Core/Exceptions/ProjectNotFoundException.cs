using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class ProjectNotFoundException : Exception
    {
        public ProjectNotFoundException(string message) : base(message)
        {
        }
    }
}