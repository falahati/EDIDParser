using System;
using EDIDParser.Enums;

namespace EDIDParser
{
    /// <summary>
    ///     Represents a standard timing
    /// </summary>
    public class StandardTiming : ITiming, IEquatable<StandardTiming>
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
        public bool Equals(ITiming other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.GetType() == GetType()
                ? Equals((StandardTiming) other)
                : (Width == other.Width) && (Height == other.Height) && (Frequency == other.Frequency);
        }

        /// <inheritdoc />
        public bool Equals(StandardTiming other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return (Ratio == other.Ratio) && (Frequency == other.Frequency) && (Width == other.Width);
        }

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
        public static bool operator ==(StandardTiming left, StandardTiming right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(StandardTiming left, StandardTiming right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            // ReSharper disable once CanBeReplacedWithTryCastAndCheckForNull
            return obj is ITiming && Equals((ITiming) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (int) Ratio;
                hashCode = (hashCode*397) ^ (int) Frequency;
                hashCode = (hashCode*397) ^ (int) Width;
                return hashCode;
            }
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Width}×{Height} @ {Frequency}";
        }
    }
}