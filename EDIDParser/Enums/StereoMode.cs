namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains possible stereo modes for a display
    /// </summary>
    public enum StereoMode : uint
    {
        /// <summary>
        ///     No stereo is supported
        /// </summary>
        NoStereo = 0,

        /// <summary>
        ///     Stereo lines fields are sequential and sync during right eye signal
        /// </summary>
        FieldSequentialSyncDuringRight = 1,

        /// <summary>
        ///     Stereo lines are similar and sync during left eye signal
        /// </summary>
        SimilarSyncDuringLeft = 2,

        /// <summary>
        ///     4-Way interleaved stereo
        /// </summary>
        Stereo4WayInterleaved = 3,

        /// <summary>
        ///     2-Way interleaved stereo
        /// </summary>
        Stereo2WayInterleaved = 4,

        /// <summary>
        ///     Right eye image is on the even lines
        /// </summary>
        RightImageOnEvenLines = 5,

        /// <summary>
        ///     Left eye image is on the even lines
        /// </summary>
        LeftImageOnEvenLines = 6,

        /// <summary>
        ///     Right and left eye images are side by side
        /// </summary>
        SideBySide = 7
    }
}