using System;
namespace Coding_Challenge.Exceptions
{
    public class FileUploadException : Exception
    {
        public FileUploadErrorType ErrorType { get; }

        public FileUploadException(FileUploadErrorType errorType, string message) : base(message)
        {
            ErrorType = errorType;
        }
    }

    public enum FileUploadErrorType
    {
        FileNotFound,
        
        InvalidFileFormat
    }

}

