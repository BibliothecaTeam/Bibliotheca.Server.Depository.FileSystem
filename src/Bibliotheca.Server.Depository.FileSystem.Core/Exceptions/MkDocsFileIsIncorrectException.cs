using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class MkDocsFileIsIncorrectException : BibliothecaException
    {
        public MkDocsFileIsIncorrectException(string message) : base(message)
        {
        }
    }
}