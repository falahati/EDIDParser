namespace EDIDParser
{
    /// <summary>
    ///     Defines the necessary properties of a display timing
    /// </summary>
    public interface ITiming
    {
        /// <summary>
        ///     Gets the timing frequency
        /// </summary>
        uint Frequency { get; }

        /// <summary>
        ///     Gets the timing height
        /// </summary>
        uint Height { get; }

        /// <summary>
        ///     Gets the timing width
        /// </summary>
        uint Width { get; }
    }
}