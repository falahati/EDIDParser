using System.Linq;
using EDIDParser.Exceptions;

namespace EDIDParser.Descriptors
{
    /// <summary>
    ///     Represents an EDID additional white point descriptor block
    /// </summary>
    public class AdditionalWhitePointDescriptor : EDIDDescriptor
    {
        private static readonly byte[] FixedHeader = {0x00, 0x00, 0x00, 0xFB, 0x00};

        private readonly int _internalOffset = 5;

        internal AdditionalWhitePointDescriptor(EDID edid, BitAwareReader reader, int offset)
            : this(edid, reader, offset, 0)
        {
        }

        internal AdditionalWhitePointDescriptor(EDID edid, BitAwareReader reader, int offset, int internalOffset)
            : base(edid, reader, offset)
        {
            if (internalOffset == 0)
            {
                IsValid = Reader.ReadBytes(Offset, 5).SequenceEqual(FixedHeader);
                if (IsValid)
                    NextDescriptor = new AdditionalWhitePointDescriptor(edid, reader, offset, internalOffset + 5);
            }
            else
            {
                IsValid = true;
                _internalOffset = internalOffset + 5;
            }
        }

        /// <summary>
        ///     Gets the gamma value (1.0-3.54)
        /// </summary>
        public double Gamma
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return (Reader.ReadByte(Offset + _internalOffset + 4) + 100)/100d;
            }
        }

        /// <summary>
        ///     Gets the white point index number
        /// </summary>
        public uint Index
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadByte(Offset + _internalOffset);
            }
        }


        /// <summary>
        ///     Gets a boolean value indicating the availability of this descriptor
        /// </summary>
        public bool IsUsed
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Index > 0;
            }
        }

        /// <summary>
        ///     Gets an other instance of the AdditionalWhitePointDescriptor type with more information, or null if not available
        /// </summary>
        public AdditionalWhitePointDescriptor NextDescriptor { get; }

        /// <summary>
        ///     Gets the white point x value
        /// </summary>
        public double WhitePointX
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (int) Reader.ReadInt(Offset + _internalOffset + 1, 2, 2);
                var most = (int) Reader.ReadByte(Offset + _internalOffset + 2);
                return ((most << 2) | least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the white point y value
        /// </summary>
        public double WhitePointY
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (int) Reader.ReadInt(Offset + _internalOffset + 1, 0, 2);
                var most = (int) Reader.ReadByte(Offset + _internalOffset + 3);
                return ((most << 2) | least)/1024d;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!IsValid)
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
            var str = $"AdditionalWhitePointDescriptor( [{Index}] ({WhitePointX}, {WhitePointY}) {Gamma} )";
            if (NextDescriptor != null)
                str += ", " + NextDescriptor;
            return str;
        }
    }
}