namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains possible digital display types
    /// </summary>
    public enum DigitalDisplayType : uint
    {
        /// <summary>
        ///     RGB 4:4:4
        /// </summary>
        RGB444 = 0,

        /// <summary>
        ///     RGB 4:4:4 + YCrCb 4:4:4
        /// </summary>
        RGB444YCrCb444 = 1,

        /// <summary>
        ///     RGB 4:4:4 + YCrCb 4:2:2
        /// </summary>
        RGB444CrCb422 = 3,

        /// <summary>
        ///     RGB 4:4:4 + YCrCb 4:4:4 + YCrCb 4:2:2
        /// </summary>
        RGB444YCrCb444YCrCb422 = 4
    }
}