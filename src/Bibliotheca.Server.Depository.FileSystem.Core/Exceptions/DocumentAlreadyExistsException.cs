using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class DocumentAlreadyExistsException : Exception
    {
        public DocumentAlreadyExistsException(string message) : base(message)
        {
        }
    }
}