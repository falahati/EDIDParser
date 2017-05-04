namespace EDIDParser
{
    /// <summary>
    ///     Represents the CIE chromaticity xy coordinates for red, green, blue, and white
    /// </summary>
    public class ChromaticityCoordinates
    {
        private readonly BitAwareReader _reader;

        internal ChromaticityCoordinates(BitAwareReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        ///     Gets the blue x value
        /// </summary>
        public float BlueX
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 6, 2);
                var most = (int) _reader.ReadByte(31);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the blue y value
        /// </summary>
        public float BlueY
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 4, 2);
                var most = (int) _reader.ReadByte(32);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the green x value
        /// </summary>
        public float GreenX
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 2, 2);
                var most = (int) _reader.ReadByte(29);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the green y value
        /// </summary>
        public float GreenY
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 0, 2);
                var most = (int) _reader.ReadByte(30);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the red x value
        /// </summary>
        public float RedX
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 6, 2);
                var most = (int) _reader.ReadByte(27);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the red y value
        /// </summary>
        public float RedY
        {
            get
            {
                var least = (int) _reader.ReadInt(25, 4, 2);
                var most = (int) _reader.ReadByte(28);
                return (most*4 + least)/1024f;
            }
        }

        /// <summary>
        ///     Gets the default white point x value
        /// </summary>
        public float WhiteX
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
        public float WhiteY
        {
            get
            {
                var least = (int) _reader.ReadInt(26, 0, 2);
                var most = (int) _reader.ReadByte(34);
                return (most*4 + least)/1024f;
            }
        }
    }
}