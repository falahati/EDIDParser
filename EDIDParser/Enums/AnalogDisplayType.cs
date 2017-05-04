namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains the possible analog display types
    /// </summary>
    public enum AnalogDisplayType : uint
    {
        /// <summary>
        ///     Grayscale display
        /// </summary>
        Monochrome = 0,

        /// <summary>
        ///     RGB colored display
        /// </summary>
        RGB = 1,

        /// <summary>
        ///     Non-RGB colored display
        /// </summary>
        NonRGB = 3,

        /// <summary>
        ///     Display type is undefined
        /// </summary>
        Undefined = 4
    }
}