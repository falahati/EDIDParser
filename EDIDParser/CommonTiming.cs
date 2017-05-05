using System;
using EDIDParser.Enums;

namespace EDIDParser
{
    /// <summary>
    ///     Represents a common timing
    /// </summary>
    public class CommonTiming : ITiming, IEquatable<CommonTiming>
    {
        internal CommonTiming(CommonTimingIdentification timingId)
        {
            Identification = timingId;
        }

        /// <summary>
        ///     Gets the common timing identification
        /// </summary>
        public CommonTimingIdentification Identification { get; }

        /// <summary>
        ///     Gets a boolean value indicating that the timing is interlaced
        /// </summary>
        public bool IsInterlaced => Identification == CommonTimingIdentification.Timing1024X768At87HzInterlaced;

        /// <inheritdoc />
        public bool Equals(CommonTiming other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Identification == other.Identification;
        }

        /// <inheritdoc />
        public bool Equals(ITiming other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.GetType() == GetType()
                ? Equals((CommonTiming) other)
                : (Width == other.Width) && (Height == other.Height) && (Frequency == other.Frequency) && !IsInterlaced;
        }

        /// <inheritdoc />
        public uint Frequency
        {
            get
            {
                switch (Identification)
                {
                    case CommonTimingIdentification.Timing800X600At56Hz:
                        return 56;
                    case CommonTimingIdentification.Timing1024X768At60Hz:
                    case CommonTimingIdentification.Timing640X480At60Hz:
                    case CommonTimingIdentification.Timing800X600At60Hz:
                        return 60;
                    case CommonTimingIdentification.Timing640X480At67Hz:
                        return 67;
                    case CommonTimingIdentification.Timing720X400At70Hz:
                        return 70;
                    case CommonTimingIdentification.Timing1024X768At72Hz:
                    case CommonTimingIdentification.Timing640X480At72Hz:
                    case CommonTimingIdentification.Timing800X600At72Hz:
                        return 72;
                    case CommonTimingIdentification.Timing1024X768At75Hz:
                    case CommonTimingIdentification.Timing1152X870At75Hz:
                    case CommonTimingIdentification.Timing1280X1024At75Hz:
                    case CommonTimingIdentification.Timing640X480At75Hz:
                    case CommonTimingIdentification.Timing800X600At75Hz:
                    case CommonTimingIdentification.Timing832X624At75Hz:
                        return 75;
                    case CommonTimingIdentification.Timing1024X768At87HzInterlaced:
                        return 87;
                    case CommonTimingIdentification.Timing720X400At88Hz:
                        return 88;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <inheritdoc />
        public uint Height
        {
            get
            {
                switch (Identification)
                {
                    case CommonTimingIdentification.Timing1024X768At60Hz:
                    case CommonTimingIdentification.Timing1024X768At72Hz:
                    case CommonTimingIdentification.Timing1024X768At75Hz:
                    case CommonTimingIdentification.Timing1024X768At87HzInterlaced:
                        return 768;
                    case CommonTimingIdentification.Timing1152X870At75Hz:
                        return 870;
                    case CommonTimingIdentification.Timing1280X1024At75Hz:
                        return 1024;
                    case CommonTimingIdentification.Timing640X480At60Hz:
                    case CommonTimingIdentification.Timing640X480At67Hz:
                    case CommonTimingIdentification.Timing640X480At72Hz:
                    case CommonTimingIdentification.Timing640X480At75Hz:
                        return 480;
                    case CommonTimingIdentification.Timing720X400At70Hz:
                    case CommonTimingIdentification.Timing720X400At88Hz:
                        return 400;
                    case CommonTimingIdentification.Timing800X600At56Hz:
                    case CommonTimingIdentification.Timing800X600At60Hz:
                    case CommonTimingIdentification.Timing800X600At72Hz:
                    case CommonTimingIdentification.Timing800X600At75Hz:
                        return 600;
                    case CommonTimingIdentification.Timing832X624At75Hz:
                        return 624;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <inheritdoc />
        public uint Width
        {
            get
            {
                switch (Identification)
                {
                    case CommonTimingIdentification.Timing1024X768At60Hz:
                    case CommonTimingIdentification.Timing1024X768At72Hz:
                    case CommonTimingIdentification.Timing1024X768At75Hz:
                    case CommonTimingIdentification.Timing1024X768At87HzInterlaced:
                        return 1024;
                    case CommonTimingIdentification.Timing1152X870At75Hz:
                        return 1152;
                    case CommonTimingIdentification.Timing1280X1024At75Hz:
                        return 1280;
                    case CommonTimingIdentification.Timing640X480At60Hz:
                    case CommonTimingIdentification.Timing640X480At67Hz:
                    case CommonTimingIdentification.Timing640X480At72Hz:
                    case CommonTimingIdentification.Timing640X480At75Hz:
                        return 640;
                    case CommonTimingIdentification.Timing720X400At70Hz:
                    case CommonTimingIdentification.Timing720X400At88Hz:
                        return 720;
                    case CommonTimingIdentification.Timing800X600At56Hz:
                    case CommonTimingIdentification.Timing800X600At60Hz:
                    case CommonTimingIdentification.Timing800X600At72Hz:
                    case CommonTimingIdentification.Timing800X600At75Hz:
                        return 800;
                    case CommonTimingIdentification.Timing832X624At75Hz:
                        return 832;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <inheritdoc />
        public static bool operator ==(CommonTiming left, CommonTiming right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(CommonTiming left, CommonTiming right)
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
            return (int) Identification;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{Width}×{Height}{(IsInterlaced ? "i" : "p")} @ {Frequency}";
        }
    }
}