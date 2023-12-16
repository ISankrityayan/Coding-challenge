using System;
namespace Coding_Challenge.Exceptions
{
    public class ApplicationDeadlineException : Exception
    {
        public ApplicationDeadlineException() { }

        public ApplicationDeadlineException(string message) : base(message) { }

        public ApplicationDeadlineException(string message, Exception innerException) : base(message, innerException) { }
    }
}

