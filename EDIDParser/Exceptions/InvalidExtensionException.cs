using System;

namespace EDIDParser.Exceptions
{
    internal class InvalidExtensionException : Exception
    {
        public InvalidExtensionException(string message) : base(message)
        {
        }
    }
}