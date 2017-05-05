using System;
using EDIDParser.Enums;

namespace EDIDParser
{
    /// <summary>
    ///     Represents a standard timing
    /// </summary>
    public class StandardTiming : ITiming
    {
        internal StandardTiming(uint width, PixelRatio ratio, uint frequency)
        {
            Width = width;
            Ratio = ratio;
            Frequency = frequency;
        }

        /// <summary>
        ///     Gets the standard timing width/height ratio
        /// </summary>
        public PixelRatio Ratio { get; }

        /// <inheritdoc />
        public uint Frequency { get; }

        /// <inheritdoc />
        public uint Height
        {
            get
            {
                switch (Ratio)
                {
                    case PixelRatio.Ratio1To1:
                        return Width;
                    case PixelRatio.Ratio16To10:
                        return Width/16*10;
                    case PixelRatio.Ratio4To3:
                        return Width/4*3;
                    case PixelRatio.Ratio5To4:
                        return Width/5*4;
                    case PixelRatio.Ratio16To9:
                        return Width/16*9;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Ratio), Ratio, null);
                }
            }
        }

        /// <inheritdoc />
        public uint Width { get; }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Width}×{Height} @ {Frequency}";
        }
    }
}