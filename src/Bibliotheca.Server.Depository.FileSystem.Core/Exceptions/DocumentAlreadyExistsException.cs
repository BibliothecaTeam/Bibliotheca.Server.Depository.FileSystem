using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class DocumentAlreadyExistsException : BibliothecaException
    {
        public DocumentAlreadyExistsException(string message) : base(message)
        {
        }
    }
}