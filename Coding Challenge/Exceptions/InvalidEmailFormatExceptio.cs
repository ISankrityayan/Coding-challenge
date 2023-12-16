using System;
namespace Coding_Challenge.Exceptions
{
	public class InvalidEmailFormatExceptio: Exception
	{
        public InvalidEmailFormatExceptio(string message) : base(message)
        {
        }
    }
}

