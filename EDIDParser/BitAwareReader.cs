using System;

namespace EDIDParser
{
    internal class BitAwareReader
    {
        public BitAwareReader(byte[] data)
        {
            Data = data;
        }

        public byte[] Data { get; }

        public bool ReadBit(int bytesOffset, int bitsOffset)
        {
            return ReadInt(bytesOffset, bitsOffset, 1) > 0;
        }

        public byte ReadByte(int bytesOffset)
        {
            return Data[bytesOffset];
        }

        public byte[] ReadBytes(int bytesOffset, int bytesCount)
        {
            var newBytes = new byte[bytesCount];
            Array.Copy(Data, bytesOffset, newBytes, 0, bytesCount);
            return newBytes;
        }

        public ulong ReadInt(int bytesOffset, int bitsOffset, int bitCounts)
        {
            if (bitsOffset >= 8)
            {
                bytesOffset = bitsOffset/8;
                bitsOffset = bitsOffset%8;
            }
            var array = ReadBytes(bytesOffset, (int) Math.Ceiling(bitCounts/8d));
            Array.Resize(ref array, 8);
            var num = (uint) (array[0] |
                              (array[1] << 0x08) |
                              (array[2] << 0x10) |
                              (array[3] << 0x18));
            var num2 = (uint) (array[4] |
                               (array[5] << 0x08) |
                               (array[6] << 0x10) |
                               (array[7] << 0x18));
            var value = ((ulong) num2 << 0x20) | num;
            if (bitsOffset > 0)
                value = value >> bitsOffset;
            bitCounts = 64 - bitCounts;
            value = (value << bitCounts) >> bitCounts;
            return value;
        }
    }
}