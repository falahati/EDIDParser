using EDIDParser.Enums;
using EDIDParser.Exceptions;

namespace EDIDParser.Descriptors
{
    /// <summary>
    ///     Represents an EDID detailed timing descriptor block
    /// </summary>
    public class DetailedTimingDescriptor : EDIDDescriptor
    {
        internal DetailedTimingDescriptor(EDID edid, BitAwareReader reader, int offset) : base(edid, reader, offset)
        {
            IsValid = Reader.ReadInt(Offset, 0, 2*8) != 0;
        }

        /// <summary>
        ///     Gets the horizontal active pixels (0–4095)
        /// </summary>
        public uint HorizontalActivePixels
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 2);
                var most = (uint) Reader.ReadInt(Offset + 4, 4, 4);
                return most*16 + least;
            }
        }

        /// <summary>
        ///     Gets the horizontal blanking pixels (0–4095)
        /// </summary>
        public uint HorizontalBlankingPixels
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 3);
                var most = (uint) Reader.ReadInt(Offset + 4, 0, 4);
                return most*16 + least;
            }
        }

        /// <summary>
        ///     Gets the horizontal border pixels (each side; total is twice this)
        /// </summary>
        public uint HorizontalBorderPixels
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadByte(Offset + 15);
            }
        }

        /// <summary>
        ///     Gets the horizontal display size, mm (0–4095 mm, 161 in)
        /// </summary>
        public uint HorizontalDisplaySize
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 12);
                var most = (uint) Reader.ReadInt(Offset + 14, 4, 4);
                return most*16 + least;
            }
        }

        /// <summary>
        ///     Gets the horizontal sync offset
        /// </summary>
        public uint HorizontalSyncOffset
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 8);
                var most = (uint) Reader.ReadInt(Offset + 11, 6, 2);
                return most*8 + least;
            }
        }

        /// <summary>
        ///     Gets the digital display's horizontal sync polarity
        /// </summary>
        /// <exception cref="AnalogDisplayException">The device is not digital.</exception>
        public DigitalSyncPolarity HorizontalSyncPolarity
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                if (!EDID.DisplayParameters.IsDigital)
                    throw new AnalogDisplayException("The device is not digital.");
                return (DigitalSyncPolarity) Reader.ReadInt(Offset + 17, 1, 1);
            }
        }

        /// <summary>
        ///     Gets the horizontal sync pulse width (0–1023)
        /// </summary>
        public uint HorizontalSyncPulse
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 9);
                var most = (uint) Reader.ReadInt(Offset + 11, 4, 2);
                return most*8 + least;
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating that the timing's scan mode is interlaced
        /// </summary>
        public bool IsInterlaced
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadBit(Offset + 17, 7);
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating that the sync is on all 3 RGB lines (else green only)
        /// </summary>
        /// <exception cref="DigitalDisplayException">The device is not analog.</exception>
        public bool IsSyncOnAllRGBLines
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                if (EDID.DisplayParameters.IsDigital)
                    throw new DigitalDisplayException("The device is not analog.");
                return Reader.ReadBit(Offset + 17, 1);
            }
        }

        /// <summary>
        ///     Gets a boolean value indicating that the vertical sync is serrated (HSync during VSync)
        /// </summary>
        /// <exception cref="DigitalDisplayException">The device is not analog.</exception>
        public bool IsVerticalSyncSerrated
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                if (EDID.DisplayParameters.IsDigital)
                    throw new DigitalDisplayException("The device is not analog.");
                return Reader.ReadBit(Offset + 17, 2);
            }
        }

        /// <summary>
        ///     Gets the pixel clock in hz. (0.01–655.35 MHz)
        /// </summary>
        public ulong PixelClock
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadInt(Offset, 0, 2*8)*10000;
            }
        }

        /// <summary>
        ///     Gets the type of the stereo signal supported
        /// </summary>
        public StereoMode StereoMode
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadInt(Offset + 17, 5, 2);
                if (least == 0)
                    return StereoMode.NoStereo;
                var most = Reader.ReadBit(Offset + 17, 0);
                return (StereoMode) ((most ? 4 : 0) + least);
            }
        }

        /// <summary>
        ///     Gets the type of display sync signal
        /// </summary>
        public SyncType SyncType
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return (SyncType) Reader.ReadInt(Offset + 17, 3, 2);
            }
        }

        /// <summary>
        ///     Gets the vertical active lines (0–4095)
        /// </summary>
        public uint VerticalActivePixels
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 5);
                var most = (uint) Reader.ReadInt(Offset + 7, 4, 4);
                return most*16 + least;
            }
        }

        /// <summary>
        ///     Gets the vertical blanking lines (0–4095)
        /// </summary>
        public uint VerticalBlankingPixels
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 6);
                var most = (uint) Reader.ReadInt(Offset + 7, 0, 4);
                return most*16 + least;
            }
        }

        /// <summary>
        ///     Gets the vertical border lines (each side; total is twice this)
        /// </summary>
        public uint VerticalBorderPixels
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadByte(Offset + 16);
            }
        }

        /// <summary>
        ///     Gets the vertical display size, mm
        /// </summary>
        public uint VerticalDisplaySize
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadByte(Offset + 13);
                var most = (uint) Reader.ReadInt(Offset + 14, 0, 4);
                return most*16 + least;
            }
        }

        /// <summary>
        ///     Gets the vertical sync offset lines (0–63)
        /// </summary>
        public uint VerticalSyncOffset
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadInt(Offset + 10, 4, 4);
                var most = (uint) Reader.ReadInt(Offset + 11, 2, 2);
                return most*8 + least;
            }
        }

        /// <summary>
        ///     Gets the digital display's vertical sync polarity
        /// </summary>
        /// <exception cref="AnalogDisplayException">The device is not digital.</exception>
        public DigitalSyncPolarity VerticalSyncPolarity
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                if (!EDID.DisplayParameters.IsDigital)
                    throw new AnalogDisplayException("The device is not digital.");
                return (DigitalSyncPolarity) Reader.ReadInt(Offset + 17, 2, 1);
            }
        }

        /// <summary>
        ///     Gets the vertical sync pulse width lines (0–63)
        /// </summary>
        public uint VerticalSyncPulse
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var least = (uint) Reader.ReadInt(Offset + 10, 0, 4);
                var most = (uint) Reader.ReadInt(Offset + 11, 0, 2);
                return most*8 + least;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!IsValid)
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
            return $"DetailedTimingDescriptor({SyncType}, {StereoMode})";
        }
    }
}