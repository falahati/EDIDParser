using System;
using System.Collections.Generic;
using System.Linq;
using EDIDParser.Enums;
using EDIDParser.Exceptions;

namespace EDIDParser.Descriptors
{
    /// <summary>
    ///     Represents an EDID additional standard timing descriptor block
    /// </summary>
    public class AdditionalStandardTimingDescriptor : EDIDDescriptor
    {
        private static readonly byte[] FixedHeader = {0x00, 0x00, 0x00, 0xFA, 0x00};

        internal AdditionalStandardTimingDescriptor(EDID edid, BitAwareReader reader, int offset)
            : base(edid, reader, offset)
        {
            if (!Reader.ReadBytes(Offset, 5).SequenceEqual(FixedHeader))
                throw new InvalidDescriptorException("The provided data does not belong to this descriptor.");
        }

        /// <summary>
        ///     Gets an additional enumerable list of timings specified in this descriptor
        /// </summary>
        public IEnumerable<StandardTiming> Timings
        {
            get
            {
                for (var i = Offset; i < Offset + 13; i += 2)
                {
                    var isValid = Reader.ReadInt(i, 0, 2*8) != 0x0101;
                    if (isValid)
                    {
                        var width = Reader.ReadInt(i, 0, 8);
                        var freq = Reader.ReadInt(i + 1, 0, 6);
                        var ratio = (int) Reader.ReadInt(i + 1, 6, 2);
                        if ((EDID.EDIDVersion < new Version(1, 3)) && (ratio == 0))
                            ratio = -1;
                        yield return new StandardTiming(((uint) width + 31)*8, (PixelRatio) ratio, (uint) freq + 60);
                    }
                }
            }
        }
    }
}