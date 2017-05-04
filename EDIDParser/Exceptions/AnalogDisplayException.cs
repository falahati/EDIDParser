using System;

namespace EDIDParser.Exceptions
{
    /// <summary>
    ///     Represents errors that occurs because of display being an analog display
    /// </summary>
    public class AnalogDisplayException : Exception
    {
        internal AnalogDisplayException(string message) : base(message)
        {
        }
    }
}