using System;
using Bibliotheca.Server.Mvc.Middleware.Diagnostics.Exceptions;

namespace Bibliotheca.Server.Depository.FileSystem.Core.Exceptions
{
    public class BranchAlreadyExistsException : BibliothecaException
    {
        public BranchAlreadyExistsException(string message) : base(message)
        {
        }
    }
}