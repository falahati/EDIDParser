namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains possible types of extension blocks
    /// </summary>
    public enum ExtensionType : uint
    {
        /// <summary>
        ///     LCD timings extension block
        /// </summary>
        LCDTimings = 0x01,

        /// <summary>
        ///     Additional timings extension block
        /// </summary>
        AdditionalTiming = 0x02,

        /// <summary>
        ///     EDID2 extension block
        /// </summary>
        EDID2 = 0x20,

        /// <summary>
        ///     Color information extension block
        /// </summary>
        ColorInformation = 0x30,

        /// <summary>
        ///     DVI features extension block
        /// </summary>
        DVIFeatures = 0x40,

        /// <summary>
        ///     Touch screen data extension block
        /// </summary>
        TouchScreenData = 0x50,

        /// <summary>
        ///     BlockMap extension block
        /// </summary>
        BlockMap = 0xF0,

        /// <summary>
        ///     Vendor specified extension block
        /// </summary>
        ManufacturerExtension = 0xFF
    }
}