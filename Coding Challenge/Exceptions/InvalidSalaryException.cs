using System;
namespace Coding_Challenge.Exceptions
{
	public class InvalidSalaryException: Exception
	{
        public InvalidSalaryException(string message) : base(message)
        {
        }
    }
}

