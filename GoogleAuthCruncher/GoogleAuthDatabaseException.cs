using System;

namespace GoogleAuthCruncher
{
    public class GoogleAuthDatabaseException : Exception
    {
        public GoogleAuthDatabaseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    public class GoogleAuthUnarchiverException : Exception
    {
        public GoogleAuthUnarchiverException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}