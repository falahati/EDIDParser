using System.Linq;
using EDIDParser.Exceptions;

namespace EDIDParser.Descriptors
{
    /// <summary>
    ///     Represents a vendor specific custom EDID descriptor block
    /// </summary>
    public class ManufacturerDescriptor : EDIDDescriptor
    {
        internal ManufacturerDescriptor(EDID edid, BitAwareReader reader, int offset) : base(edid, reader, offset)
        {
            if (Reader.ReadBytes(Offset, 3).Any(b => b != 0))
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
            if (Reader.ReadByte(Offset + 4) != 0)
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
            if (Reader.ReadByte(Offset + 3) > 15)
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
        }

        /// <summary>
        ///     Gets the raw 13byte binary data of the block
        /// </summary>
        public byte[] Data => Reader.ReadBytes(Offset + 5, 13);

        /// <summary>
        ///     Gets the descriptor type identification number
        /// </summary>
        public uint DescriptorCode => Reader.ReadByte(Offset + 3);

        /// <inheritdoc />
        public override string ToString()
        {
            return $"ManufacturerDescriptor({DescriptorCode:X2})";
        }
    }
}