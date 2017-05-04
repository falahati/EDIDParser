using System;

namespace EDIDParser.Exceptions
{
    internal class InvalidDescriptorException : Exception
    {
        internal InvalidDescriptorException(string message) : base(message)
        {
        }
    }
}