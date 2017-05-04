using System;

namespace EDIDParser.Exceptions
{
    /// <summary>
    ///     Represents errors that occurs because of display being a projector
    /// </summary>
    public class ProjectorDisplayException : Exception
    {
        internal ProjectorDisplayException(string message) : base(message)
        {
        }
    }
}