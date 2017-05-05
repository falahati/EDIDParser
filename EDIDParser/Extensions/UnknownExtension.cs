namespace EDIDParser.Extensions
{
    /// <summary>
    ///     Represents an unknown EDID Extension Block
    /// </summary>
    public class UnknownExtension : EDIDExtension
    {
        internal UnknownExtension(EDID edid, BitAwareReader reader, int offset) : base(edid, reader, offset)
        {
        }

        /// <summary>
        ///     Gets a 125 byte raw data of this block
        /// </summary>
        public byte[] Data => Reader.ReadBytes(Offset + 2, 125);

        /// <summary>
        ///     Gets the block revision number
        /// </summary>
        public uint Revision => Reader.ReadByte(Offset + 1);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"UnknownExtension({Type} v{Revision})";
        }
    }
}