using System;

namespace EDIDParser.Exceptions
{
    /// <summary>
    ///     Represents errors that occurs because of invalid EDID data
    /// </summary>
    public class InvalidEDIDException : Exception
    {
        internal InvalidEDIDException(string message) : base(message)
        {
        }
    }
}