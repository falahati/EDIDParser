using System.Linq;
using EDIDParser.Exceptions;

namespace EDIDParser.Descriptors
{
    /// <summary>
    ///     Represents an EDID monitor range limit descriptor block
    /// </summary>
    public class MonitorRangeLimitsDescriptor : EDIDDescriptor
    {
        private static readonly byte[] FixedHeader = {0x00, 0x00, 0x00, 0xFD, 0x00};

        internal MonitorRangeLimitsDescriptor(EDID edid, BitAwareReader reader, int offset) : base(edid, reader, offset)
        {
            if (!Reader.ReadBytes(Offset, 5).SequenceEqual(FixedHeader))
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
        }

        /// <summary>
        ///     Gets the GTF C value (0–127.5)
        /// </summary>
        /// <exception cref="ExtendedTimingNotAvailable">Secondary GTF is not supported.</exception>
        public double GTFC
        {
            get
            {
                if (!IsSecondaryGTFSupported)
                    throw new ExtendedTimingNotAvailable("Secondary GTF is not supported.");
                return Reader.ReadByte(Offset + 13)/2d;
            }
        }

        /// <summary>
        ///     Gets the GTF J value (0–127.5)
        /// </summary>
        /// <exception cref="ExtendedTimingNotAvailable">Secondary GTF is not supported.</exception>
        public double GTFJ
        {
            get
            {
                if (!IsSecondaryGTFSupported)
                    throw new ExtendedTimingNotAvailable("Secondary GTF is not supported.");
                return Reader.ReadByte(Offset + 17)/2d;
            }
        }

        /// <summary>
        ///     Gets the GTF K value (0–255)
        /// </summary>
        /// <exception cref="ExtendedTimingNotAvailable">Secondary GTF is not supported.</exception>
        public byte GTFK
        {
            get
            {
                if (!IsSecondaryGTFSupported)
                    throw new ExtendedTimingNotAvailable("Secondary GTF is not supported.");
                return Reader.ReadByte(Offset + 16);
            }
        }

        /// <summary>
        ///     Gets the GTF M value (0–65535)
        /// </summary>
        /// <exception cref="ExtendedTimingNotAvailable">Secondary GTF is not supported.</exception>
        public ushort GTFM
        {
            get
            {
                if (!IsSecondaryGTFSupported)
                    throw new ExtendedTimingNotAvailable("Secondary GTF is not supported.");
                return (ushort) Reader.ReadInt(Offset + 14, 0, 2*8);
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating that the secondary GTF is supported
        /// </summary>
        public bool IsSecondaryGTFSupported => Reader.ReadByte(Offset + 10) == 0x02;

        /// <summary>
        ///     Gets the maximum horizontal field rate
        /// </summary>
        public uint MaximumHorizontalFieldRate => Reader.ReadByte(Offset + 8)*1000u;

        /// <summary>
        ///     Gets the maximum pixel clock rate in hz (10–2550 MHz)
        /// </summary>
        public ulong MaximumPixelClockRate => Reader.ReadByte(Offset + 9)*10000000ul;

        /// <summary>
        ///     Gets the maximum vertical field rate
        /// </summary>
        public uint MaximumVerticalFieldRate => Reader.ReadByte(Offset + 6);

        /// <summary>
        ///     Gets the minimum horizontal field rate
        /// </summary>
        public uint MinimumHorizontalFieldRate => Reader.ReadByte(Offset + 7)*1000u;

        /// <summary>
        ///     Gets the minimum vertical field rate
        /// </summary>
        public uint MinimumVerticalFieldRate => Reader.ReadByte(Offset + 5);

        /// <summary>
        ///     Gets the start frequency for the secondary curve in hz (0–510 kHz)
        /// </summary>
        /// <exception cref="ExtendedTimingNotAvailable">Secondary GTF is not supported.</exception>
        public uint SecondaryCurveStartFrequency
        {
            get
            {
                if (!IsSecondaryGTFSupported)
                    throw new ExtendedTimingNotAvailable("Secondary GTF is not supported.");
                return (uint) Reader.ReadByte(Offset + 12)*2000;
            }
        }
    }
}