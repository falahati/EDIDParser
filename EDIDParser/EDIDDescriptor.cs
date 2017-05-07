using System;
using System.Linq;
using System.Reflection;

namespace EDIDParser
{
    /// <summary>
    ///     Represents an EDID Descriptor Block
    /// </summary>
    public abstract class EDIDDescriptor : IEquatable<EDIDDescriptor>
    {
        internal readonly EDID EDID;
        internal readonly int Offset;
        internal readonly BitAwareReader Reader;

        internal EDIDDescriptor(EDID edid, BitAwareReader reader, int offset)
        {
            EDID = edid;
            Reader = reader;
            Offset = offset;
        }

        /// <inheritdoc />
        public bool Equals(EDIDDescriptor other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Reader.ReadBytes(Offset, 18).SequenceEqual(other.Reader.ReadBytes(other.Offset, 18));
        }

        /// <inheritdoc />
        public static bool operator ==(EDIDDescriptor left, EDIDDescriptor right)
        {
            return Equals(left, right);
        }

        /// <inheritdoc />
        public static bool operator !=(EDIDDescriptor left, EDIDDescriptor right)
        {
            return !Equals(left, right);
        }

        internal static EDIDDescriptor FromData(EDID edid, BitAwareReader reader, int offset)
        {
            var types =
                Assembly.GetAssembly(typeof(EDIDDescriptor))
                    .GetTypes()
                    .Where(t => t.IsSubclassOf(typeof(EDIDDescriptor)));
            foreach (var type in types)
            {
                EDIDDescriptor value = null;
                try
                {
                    value =
                        Activator.CreateInstance(type, BindingFlags.NonPublic | BindingFlags.Instance, null,
                            new object[] {edid, reader, offset}, null) as EDIDDescriptor;
                }
                catch
                {
                    // ignored
                }
                if (value != null)
                    return value;
            }
            return null;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var other = obj as EDIDDescriptor;
            return (other != null) && Equals(other);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Reader?.ReadBytes(Offset, 18).GetHashCode() ?? 0;
        }
    }
}