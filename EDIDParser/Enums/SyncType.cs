namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains possible values for the detailed timing descriptor block's sync type feature field
    /// </summary>
    public enum SyncType : uint
    {
        /// <summary>
        ///     Analog display composite sync
        /// </summary>
        AnalogComposite = 0,

        /// <summary>
        ///     Analog display bipolar composite sync
        /// </summary>
        BipolarAnalogComposite = 1,

        /// <summary>
        ///     Digital display composite sync on HSync
        /// </summary>
        DigitalCompositeOnHSync = 2,

        /// <summary>
        ///     Digital display separate sync
        /// </summary>
        DigitalSeparate = 3
    }
}