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

        internal AdditionalWhitePointDescriptor(EDID edid, BitAwareReader reader, int offset,
            bool firstDescriptor = true) : base(edid, reader, offset)
        {
            if (firstDescriptor)
            {
                if (!Reader.ReadBytes(Offset, 5).SequenceEqual(FixedHeader))
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                NextDescriptor = new AdditionalWhitePointDescriptor(edid, reader, offset + 10, false);
                Offset += 5;
            }
        }

        /// <summary>
        ///     Gets the gamma value (1.0–3.54)
        /// </summary>
        public double Gamma => (Reader.ReadByte(Offset + 4) + 100)/100d;

        /// <summary>
        ///     Gets the white point index number
        /// </summary>
        public uint Index => Reader.ReadByte(Offset);


        /// <summary>
        ///     Gets a boolean value indicating the availability of this descriptor
        /// </summary>
        public bool IsUsed => Index > 0;

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
                var least = (int) Reader.ReadInt(Offset + 1, 2, 2);
                var most = (int) Reader.ReadByte(Offset + 2);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the white point y value
        /// </summary>
        public double WhitePointY
        {
            get
            {
                var least = (int) Reader.ReadInt(Offset + 1, 0, 2);
                var most = (int) Reader.ReadByte(Offset + 3);
                return (most*4 + least)/1024d;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var str = $"AdditionalWhitePointDescriptor( [{Index}] ({WhitePointX}, {WhitePointY}) {Gamma} )";
            if (NextDescriptor != null)
                str += ", " + NextDescriptor;
            return str;
        }
    }
}