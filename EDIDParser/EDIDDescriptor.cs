namespace EDIDParser
{
    /// <summary>
    ///     Represents an EDID Descriptor Block
    /// </summary>
    public abstract class EDIDDescriptor
    {
        internal readonly EDID EDID;
        internal readonly BitAwareReader Reader;
        internal int Offset;

        internal EDIDDescriptor(EDID edid, BitAwareReader reader, int offset)
        {
            EDID = edid;
            Reader = reader;
            Offset = offset;
        }
    }
}