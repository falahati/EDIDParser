using System;
using System.Linq;

namespace EDIDParser
{
    /// <summary>
    ///     Represents the CIE chromaticity xy coordinates for red, green, blue, and white
    /// </summary>
    public class ChromaticityCoordinates : IEquatable<ChromaticityCoordinates>
    {
        private readonly BitAwareReader _reader;

        internal ChromaticityCoordinates(BitAwareReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        ///     Gets the blue x value
        /// </summary>
        public double BlueX
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 6, 2);
                var most = (int) _reader.ReadByte(31);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the blue y value
        /// </summary>
        public double BlueY
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 4, 2);
                var most = (int) _reader.ReadByte(32);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the green x value
        /// </summary>
        public double GreenX
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 2, 2);
                var most = (int) _reader.ReadByte(29);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the green y value
        /// </summary>
        public double GreenY
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 0, 2);
                var most = (int) _reader.ReadByte(30);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the red x value
        /// </summary>
        public double RedX
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 6, 2);
                var most = (int) _reader.ReadByte(27);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the red y value
        /// </summary>
        public double RedY
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 4, 2);
                var most = (int) _reader.ReadByte(28);
                return (most*4 + least)/1024d;
            }
        }

        /// <summary>
        ///     Gets the default white point x value
        /// </summary>
        public double WhiteX
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 2, 2);
                var most = (int) _reader.ReadByte(33);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the default white point y value
        /// </summary>
        public double WhiteY
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 0, 2);
                var most = (int) _reader.ReadByte(34);
                return (most*4 + least)/1024d;
            }
        }

        /// <inheritdoc />
        public bool Equals(ChromaticityCoordinates other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _reader.ReadBytes(25, 10).SequenceEqual(other._reader.ReadBytes(25, 10));
        }

        /// <inheritdoc />
        public static bool operator ==(ChromaticityCoordinates left, ChromaticityCoordinates right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(ChromaticityCoordinates left, ChromaticityCoordinates right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((ChromaticityCoordinates) obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _reader?.ReadBytes(25, 10).GetHashCode() ?? 0;
        }


        /// <inheritdoc />
        public override string ToString()
        {
            return
                $"RGBW([{RedX:0.000}, {RedY:0.000}] [{GreenX:0.000}, {GreenY:0.000}] [{BlueX:0.000}, {BlueY:0.000}] [{WhiteX:0.000}, {WhiteY:0.000}])";
        }
    }
}