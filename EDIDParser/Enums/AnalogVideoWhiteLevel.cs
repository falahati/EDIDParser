namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains possible values of the analog display's white and sync levels relative to blank
    /// </summary>
    public enum AnalogVideoWhiteLevel : uint
    {
        /// <summary>
        ///     +0.7/−0.3 V
        /// </summary>
        White07OnMinus03V = 0,

        /// <summary>
        ///     +0.714/−0.286 V
        /// </summary>
        White0714OnMinus0286V = 1,

        /// <summary>
        ///     +1.0/−0.4 V
        /// </summary>
        White1OnMinus04V = 2,

        /// <summary>
        ///     +0.7/0 V
        /// </summary>
        White07On0V = 3
    }
}