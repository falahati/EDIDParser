using System.Linq;
using EDIDParser.Enums;
using EDIDParser.Exceptions;

namespace EDIDParser
{
    /// <summary>
    ///     Represents an EDID Extension Block
    /// </summary>
    public abstract class EDIDExtension
    {
        internal readonly EDID EDID;
        internal readonly BitAwareReader Reader;
        internal int Offset;

        internal EDIDExtension(EDID edid, BitAwareReader reader, int offset)
        {
            EDID = edid;
            Reader = reader;
            Offset = offset;
            if (reader.Data.Length - offset < 128)
                throw new InvalidExtensionException("Extension data must be exactly 128 bytes.");
            if (reader.Data.Skip(offset).Take(128).Aggregate(0, (i, b) => (i + b)%256) > 0)
                throw new InvalidExtensionException("Extension checksum failed.");
        }

        /// <summary>
        ///     Gets the extension block type
        /// </summary>
        public ExtensionType Type => (ExtensionType) Reader.ReadByte(Offset);
    }
}