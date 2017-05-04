using System.Collections.Generic;
using System.Linq;
using EDIDParser.Enums;
using EDIDParser.Exceptions;

namespace EDIDParser.Extensions
{
    /// <summary>
    ///     Represents an EDID block map extension block
    /// </summary>
    public class BlockMapExtension : EDIDExtension
    {
        internal BlockMapExtension(EDID edid, BitAwareReader reader, int offset) : base(edid, reader, offset)
        {
            if (Type != ExtensionType.BlockMap)
                throw new InvalidExtensionException("Invalid extension type.");
        }

        /// <summary>
        ///     Gets an enumerable list of next 126 block types
        /// </summary>
        public IEnumerable<ExtensionType> ExtensionTypes
            => Reader.ReadBytes(Offset + 1, 126).Where(b => b > 0).Select(b => (ExtensionType) b);
    }
}