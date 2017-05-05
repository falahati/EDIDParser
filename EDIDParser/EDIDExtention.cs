using System;
using System.Linq;
using EDIDParser.Enums;
using EDIDParser.Exceptions;

namespace EDIDParser
{
    /// <summary>
    ///     Represents an EDID Extension Block
    /// </summary>
    public abstract class EDIDExtension : IEquatable<EDIDExtension>
    {
        internal readonly EDID EDID;
        internal readonly int Offset;
        internal readonly BitAwareReader Reader;

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


        /// <inheritdoc />
        public bool Equals(EDIDExtension other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Reader.ReadBytes(Offset, 128).SequenceEqual(other.Reader.ReadBytes(other.Offset, 128));
        }

        /// <inheritdoc />
        public static bool operator ==(EDIDExtension left, EDIDExtension right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(EDIDExtension left, EDIDExtension right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as EDIDExtension;
            return (other != null) && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Reader?.ReadBytes(Offset, 128).GetHashCode() ?? 0;
        }
    }
}