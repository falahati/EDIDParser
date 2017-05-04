using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using EDIDParser.Descriptors;
using EDIDParser.Enums;
using EDIDParser.Exceptions;
using EDIDParser.Extensions;

namespace EDIDParser
{
    /// <summary>
    ///     Represents a Extended Display Identification Data instance
    /// </summary>
    public class EDID
    {
        private static readonly byte[] FixedHeader = {0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00};
        private readonly BitAwareReader _reader;

        /// <summary>
        ///     Creates a new EDID instance with the provided EDID binary data
        /// </summary>
        /// <param name="data">An array of bytes holding the EDID binary data</param>
        /// <exception cref="InvalidEDIDException">Invalid EDID binary data.</exception>
        public EDID(byte[] data)
        {
            if (data.Length < 128)
                throw new InvalidEDIDException("EDID data must be at least 128 bytes.");
            _reader = new BitAwareReader(data);
            if (!_reader.ReadBytes(0, 8).SequenceEqual(FixedHeader))
                throw new InvalidEDIDException("EDID header missing.");
            if (_reader.Data.Take(128).Aggregate(0, (i, b) => (i + b)%256) > 0)
                throw new InvalidEDIDException("EDID checksum failed.");
            if (EDIDVersion.Major != 1)
                throw new InvalidEDIDException("Invalid EDID major version.");
            if (EDIDVersion.Minor == 0)
                throw new InvalidEDIDException("Invalid EDID minor version.");
            DisplayParameters = new DisplayParameters(this, _reader);
        }

        /// <summary>
        ///     Gets the enumerable list of descriptor blocks
        /// </summary>
        public IEnumerable<EDIDDescriptor> Descriptors
        {
            get
            {
                var types = new[]
                {
                    typeof(AdditionalStandardTimingDescriptor),
                    typeof(AdditionalWhitePointDescriptor),
                    typeof(DetailedTimingDescriptor),
                    typeof(MonitorRangeLimitsDescriptor),
                    typeof(StringDescriptor),
                    typeof(ManufacturerDescriptor)
                };
                for (var i = 54; i < 126; i += 18)
                    foreach (var type in types)
                    {
                        EDIDDescriptor value = null;
                        try
                        {
                            value =
                                Activator.CreateInstance(type, BindingFlags.NonPublic | BindingFlags.Instance, null,
                                    new object[] {this, _reader, i}, null) as EDIDDescriptor;
                        }
                        catch
                        {
                            // ignored
                        }
                        if (value != null)
                            yield return value;
                    }
            }
        }

        /// <summary>
        ///     Gets an instance of DisplayParameters type representing the basic display parameters
        /// </summary>
        public DisplayParameters DisplayParameters { get; }

        /// <summary>
        ///     Gets the EDID specification version number
        /// </summary>
        public Version EDIDVersion => new Version(_reader.ReadByte(18), _reader.ReadByte(19));


        /// <summary>
        ///     Gets the enumerable list of extensions blocks
        /// </summary>
        public IEnumerable<EDIDExtension> Extensions
        {
            get
            {
                var types = new[]
                {
                    typeof(BlockMapExtension),
                    typeof(UnknownExtension)
                };
                for (var i = 1; i <= NumberOfExtensions; i++)
                    foreach (var type in types)
                    {
                        EDIDExtension value = null;
                        try
                        {
                            value =
                                Activator.CreateInstance(type, BindingFlags.NonPublic | BindingFlags.Instance, null,
                                    new object[] {this, _reader, i*128}, null) as EDIDExtension;
                        }
                        catch
                        {
                            // ignored
                        }
                        if (value != null)
                            yield return value;
                    }
            }
        }

        /// <summary>
        ///     Gets the date of manufacturing of the device with accuracy of +-6 days
        /// </summary>
        public DateTime ManufactureDate
        {
            get
            {
                var jan1 = new DateTime((int) ManufactureYear, 1, 1);
                var daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

                var firstThursday = jan1.AddDays(daysOffset);
                var cal = CultureInfo.CurrentCulture.Calendar;
                var firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

                var weekNum = ManufactureWeek;
                if (firstWeek <= 1)
                    weekNum -= 1;
                var result = firstThursday.AddDays(weekNum*7);
                return result.AddDays(-3);
            }
        }

        /// <summary>
        ///     Gets the manufacturer identification assigned by Microsoft to the device vendors in string
        /// </summary>
        public string ManufacturerCode
        {
            get
            {
                var edidCode = ManufacturerId;
                edidCode = ((edidCode & 0xff00) >> 8) | ((edidCode & 0x00ff) << 8);
                var byte1 = (byte) 'A' + ((edidCode >> 0) & 0x1f) - 1;
                var byte2 = (byte) 'A' + ((edidCode >> 5) & 0x1f) - 1;
                var byte3 = (byte) 'A' + ((edidCode >> 10) & 0x1f) - 1;
                return $"{Convert.ToChar(byte3)}{Convert.ToChar(byte2)}{Convert.ToChar(byte1)}";
            }
        }

        /// <summary>
        ///     Gets the manufacturer identification assigned by Microsoft to the device vendors as a numberic value
        /// </summary>
        public uint ManufacturerId => (uint) _reader.ReadInt(8, 0, 2*8);

        /// <summary>
        ///     Gets the week of the device device production date as a number between 1 and 54
        /// </summary>
        /// <exception cref="ManufactureDateMissingException">Manufacture date is not available.</exception>
        public uint ManufactureWeek
        {
            get
            {
                var week = (uint) _reader.ReadByte(16);
                if (week == 0)
                    throw new ManufactureDateMissingException("Manufacture date is not available.");
                return week;
            }
        }

        /// <summary>
        ///     Gets the year of the device device production date as a number between 1990 and 2245
        /// </summary>
        /// <exception cref="ManufactureDateMissingException">Manufacture date is not available.</exception>
        public uint ManufactureYear
        {
            get
            {
                if (ManufactureWeek > 0)
                    return ProductYear;
                throw new ManufactureDateMissingException("Manufacture date is not available.");
            }
        }

        /// <summary>
        ///     Gets the expected number of extension blocks
        /// </summary>
        public uint NumberOfExtensions => _reader.ReadByte(126);

        /// <summary>
        ///     Gets the product identification code assigned by Microsoft to this series of devices
        /// </summary>
        public uint ProductCode => (uint) _reader.ReadInt(10, 0, 2*8);

        /// <summary>
        ///     Gets the year of the device device production or the model year of this product as a number between 1990 and 2245
        /// </summary>
        public uint ProductYear => (uint) _reader.ReadByte(17) + 1990;

        /// <summary>
        ///     Gets the numberic serial number of the device
        /// </summary>
        public uint SerialNumber => (uint) _reader.ReadInt(12, 0, 4*8);

        /// <summary>
        ///     Gets the enumerable list of valid timing combinations
        /// </summary>
        public IEnumerable<ITiming> Timings
        {
            get
            {
                var commonTiming = (CommonTimingIdentification) _reader.ReadInt(35, 0, 3*8);
                foreach (Enum value in Enum.GetValues(typeof(CommonTimingIdentification)))
                    if (commonTiming.HasFlag(value))
                        yield return new CommonTiming((CommonTimingIdentification) value);
                for (var i = 38; i < 54; i += 2)
                {
                    var isValid = _reader.ReadInt(i, 0, 2*8) != 0x0101;
                    if (isValid)
                    {
                        var width = _reader.ReadInt(i, 0, 8);
                        var freq = _reader.ReadInt(i + 1, 0, 6);
                        var ratio = (int) _reader.ReadInt(i + 1, 6, 2);
                        if ((EDIDVersion < new Version(1, 3)) && (ratio == 0))
                            ratio = -1;
                        yield return new StandardTiming(((uint) width + 31)*8, (PixelRatio) ratio, (uint) freq + 60);
                    }
                }
            }
        }
    }
}