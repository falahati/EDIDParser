using System;

namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains a list of common timing identification flags
    /// </summary>
    [Flags]
    public enum CommonTimingIdentification : uint
    {
        /// <summary>
        ///     1024×768 @ 60 Hz
        /// </summary>
        Timing1024X768At60Hz = 1 << 11,

        /// <summary>
        ///     1024×768 @ 72 Hz
        /// </summary>
        Timing1024X768At72Hz = 1 << 10,

        /// <summary>
        ///     1024×768 @ 75 Hz
        /// </summary>
        Timing1024X768At75Hz = 1 << 9,

        /// <summary>
        ///     1024×768 @ 87 Hz, interlaced (1024×768i)
        /// </summary>
        Timing1024X768At87HzInterlaced = 1 << 12,

        /// <summary>
        ///     1152x870 @ 75 Hz (Apple Macintosh II)
        /// </summary>
        Timing1152X870At75Hz = 1 << 23,

        /// <summary>
        ///     1280×1024 @ 75 Hz
        /// </summary>
        Timing1280X1024At75Hz = 1 << 8,

        /// <summary>
        ///     640×480 @ 60 Hz
        /// </summary>
        Timing640X480At60Hz = 1 << 5,

        /// <summary>
        ///     640×480 @ 67 Hz
        /// </summary>
        Timing640X480At67Hz = 1 << 4,

        /// <summary>
        ///     640×480 @ 72 Hz
        /// </summary>
        Timing640X480At72Hz = 1 << 3,

        /// <summary>
        ///     640×480 @ 75 Hz
        /// </summary>
        Timing640X480At75Hz = 1 << 2,

        /// <summary>
        ///     720×400 @ 70 Hz
        /// </summary>
        Timing720X400At70Hz = 1 << 7,

        /// <summary>
        ///     720×400 @ 88 Hz
        /// </summary>
        Timing720X400At88Hz = 1 << 6,

        /// <summary>
        ///     800×600 @ 56 Hz
        /// </summary>
        Timing800X600At56Hz = 1 << 1,

        /// <summary>
        ///     800×600 @ 60 Hz
        /// </summary>
        Timing800X600At60Hz = 1 << 0,

        /// <summary>
        ///     800×600 @ 72 Hz
        /// </summary>
        Timing800X600At72Hz = 1 << 15,

        /// <summary>
        ///     800×600 @ 75 Hz
        /// </summary>
        Timing800X600At75Hz = 1 << 14,

        /// <summary>
        ///     832×624 @ 75 Hz
        /// </summary>
        Timing832X624At75Hz = 1 << 13
    }
}