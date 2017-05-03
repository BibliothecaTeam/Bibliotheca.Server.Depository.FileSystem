using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class ProjectAlreadyExistsException : BibliothecaException
    {
        public ProjectAlreadyExistsException(string message) : base(message)
        {
        }
    }
}