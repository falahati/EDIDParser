using System.Linq;
using System.Text;
using EDIDParser.Enums;
using EDIDParser.Exceptions;

namespace EDIDParser.Descriptors
{
    /// <summary>
    ///     Represents an EDID string descriptor block
    /// </summary>
    public class StringDescriptor : EDIDDescriptor
    {
        private static readonly byte[] MonitorNameHeader = {0x00, 0x00, 0x00, 0xFC, 0x00};
        private static readonly byte[] MonitorSerialHeader = {0x00, 0x00, 0x00, 0xFF, 0x00};
        private static readonly byte[] UnspecifiedStringHeader = {0x00, 0x00, 0x00, 0xFE, 0x00};

        internal StringDescriptor(EDID edid, BitAwareReader reader, int offset) : base(edid, reader, offset)
        {
            IsValid = true;
            if (Reader.ReadBytes(Offset, 5).SequenceEqual(MonitorNameHeader))
                Type = StringDescriptorType.MonitorName;
            else if (Reader.ReadBytes(Offset, 5).SequenceEqual(MonitorSerialHeader))
                Type = StringDescriptorType.MonitorSerialNumber;
            else if (Reader.ReadBytes(Offset, 5).SequenceEqual(UnspecifiedStringHeader))
                Type = StringDescriptorType.UnspecifiedText;
            else
                IsValid = false;
        }

        /// <summary>
        ///     Gets the type of the data stored as a string
        /// </summary>
        public StringDescriptorType Type { get; } = StringDescriptorType.InvalidData;

        /// <summary>
        ///     Gets the string data
        /// </summary>
        public string Value
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                var bytes = Reader.ReadBytes(Offset + 5, 13);
                return Encoding.ASCII.GetString(bytes).Trim();
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!IsValid)
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
            return $"StringDescriptor({Type}: {Value})";
        }
    }
}