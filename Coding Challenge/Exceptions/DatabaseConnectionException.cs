using System;
namespace Coding_Challenge.Exceptions
{
    public class DatabaseConnectionException : Exception
    {
        public DatabaseConnectionException() { }

        public DatabaseConnectionException(string message) : base(message) { }

        public DatabaseConnectionException(string message, Exception innerException) : base(message, innerException) { }
    }
}

