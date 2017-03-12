using System;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class MkDocsFileIsIncorrectException : Exception
    {
        public MkDocsFileIsIncorrectException(string message) : base(message)
        {
        }
    }
}