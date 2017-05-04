using System;

namespace EDIDParser.Exceptions
{
    /// <summary>
    ///     Represents errors that occurs because of missing gamma information
    /// </summary>
    public class GammaValueNotAvailableException : Exception
    {
        internal GammaValueNotAvailableException(string message) : base(message)
        {
        }
    }
}