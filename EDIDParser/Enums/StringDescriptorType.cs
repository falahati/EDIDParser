namespace EDIDParser.Enums
{
    /// <summary>
    ///     Contains possible values for the string descriptor block's string type field
    /// </summary>
    public enum StringDescriptorType : uint
    {        
        /// <summary>
        ///     Block contains no valid string
        /// </summary>
        InvalidData,

        /// <summary>
        ///     Block contains the monitor serial number
        /// </summary>
        MonitorSerialNumber,

        /// <summary>
        ///     Block contains the monitor name
        /// </summary>
        MonitorName,

        /// <summary>
        ///     The type of the string is unspecified
        /// </summary>
        UnspecifiedText
    }
}