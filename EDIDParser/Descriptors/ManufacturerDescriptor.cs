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
            IsValid = Reader.ReadBytes(Offset, 3).Any(b => b == 0) &&
                      (Reader.ReadByte(Offset + 3) < 16) &&
                      (Reader.ReadByte(Offset + 4) == 0);
        }

        /// <summary>
        ///     Gets the raw 13byte binary data of the block
        /// </summary>
        public byte[] Data
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadBytes(Offset + 5, 13);
            }
        }

        /// <summary>
        ///     Gets the descriptor type identification number
        /// </summary>
        public uint DescriptorCode
        {
            get
            {
                if (!IsValid)
                    throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
                return Reader.ReadByte(Offset + 3);
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            if (!IsValid)
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
            return $"ManufacturerDescriptor({DescriptorCode:X2})";
        }
    }
}