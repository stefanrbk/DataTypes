﻿namespace System
{
    /// <summary>
    /// Helper class for Half conversions and some low level operations.
    /// This class is internally used in the Half class.
    /// </summary>
    /// <remarks>
    /// References:
    ///     - Code retrieved from http://sourceforge.net/p/csharp-half/code/HEAD/tree/ on 2015-12-04
    ///     - Fast Half Float Conversions, Jeroen van der Zijp, link: http://www.fox-toolkit.org/ftp/fasthalffloatconversion.pdf
    /// </remarks>
    internal static class HalfHelper
    {
        private static readonly uint[] _mantissaTable = GenerateMantissaTable();
        private static readonly uint[] _exponentTable = GenerateExponentTable();
        private static readonly ushort[] _offsetTable = GenerateOffsetTable();
        private static readonly ushort[] _baseTable = GenerateBaseTable();
        private static readonly sbyte[] _shiftTable = GenerateShiftTable();

        // Transforms the subnormal representation to a normalized one.
        private static uint ConvertMantissa(int i)
        {
            var m = (uint)(i << 13); // Zero pad mantissa bits
            var e = 0u; // Zero exponent

            // While not normalized
            while ((m & 0x00800000) == 0)
            {
                e -= 0x00800000; // Decrement exponent (1<<23)
                m <<= 1; // Shift mantissa
            }
            m &= unchecked((uint)~0x00800000); // Clear leading 1 bit
            e += 0x38800000; // Adjust bias ((127-14)<<23)
            return m | e; // Return combined number
        }

        private static uint[] GenerateMantissaTable()
        {
            var mantissaTable = new uint[2048];
            mantissaTable[0] = 0;
            for (var i = 1; i < 1024; i++)
                mantissaTable[i] = ConvertMantissa(i);

            for (var i = 1024; i < 2048; i++)
                mantissaTable[i] = (uint)(0x38000000 + ((i - 1024) << 13));

            return mantissaTable;
        }

        private static uint[] GenerateExponentTable()
        {
            var exponentTable = new uint[64];
            exponentTable[0] = 0;
            for (var i = 1; i < 31; i++)
                exponentTable[i] = (uint)(i << 23);

            exponentTable[31] = 0x47800000;
            exponentTable[32] = 0x80000000;
            for (var i = 33; i < 63; i++)
                exponentTable[i] = (uint)(0x80000000 + ((i - 32) << 23));

            exponentTable[63] = 0xc7800000;

            return exponentTable;
        }

        private static ushort[] GenerateOffsetTable()
        {
            var offsetTable = new ushort[64];
            offsetTable[0] = 0;
            for (var i = 1; i < 32; i++)
                offsetTable[i] = 1024;

            offsetTable[32] = 0;
            for (var i = 33; i < 64; i++)
                offsetTable[i] = 1024;

            return offsetTable;
        }

        private static ushort[] GenerateBaseTable()
        {
            var baseTable = new ushort[512];
            for (var i = 0; i < 256; ++i)
            {
                var e = (sbyte)(127 - i);
                if (e > 24)
                { // Very small numbers map to zero
                    baseTable[i | 0x000] = 0x0000;
                    baseTable[i | 0x100] = 0x8000;
                }
                else if (e > 14)
                { // Small numbers map to denorms
                    baseTable[i | 0x000] = (ushort)(0x0400 >> (18 + e));
                    baseTable[i | 0x100] = (ushort)((0x0400 >> (18 + e)) | 0x8000);
                }
                else if (e >= -15)
                { // Normal numbers just lose precision
                    baseTable[i | 0x000] = (ushort)((15 - e) << 10);
                    baseTable[i | 0x100] = (ushort)(((15 - e) << 10) | 0x8000);
                }
                else if (e > -128)
                { // Large numbers map to Infinity
                    baseTable[i | 0x000] = 0x7c00;
                    baseTable[i | 0x100] = 0xfc00;
                }
                else
                { // Infinity and NaN's stay Infinity and NaN's
                    baseTable[i | 0x000] = 0x7c00;
                    baseTable[i | 0x100] = 0xfc00;
                }
            }

            return baseTable;
        }

        private static sbyte[] GenerateShiftTable()
        {
            var shiftTable = new sbyte[512];
            for (var i = 0; i < 256; ++i)
            {
                var e = (sbyte)(127 - i);
                if (e > 24)
                { // Very small numbers map to zero
                    shiftTable[i | 0x000] = 24;
                    shiftTable[i | 0x100] = 24;
                }
                else if (e > 14)
                { // Small numbers map to denorms
                    shiftTable[i | 0x000] = (sbyte)(e - 1);
                    shiftTable[i | 0x100] = (sbyte)(e - 1);
                }
                else if (e >= -15)
                { // Normal numbers just lose precision
                    shiftTable[i | 0x000] = 13;
                    shiftTable[i | 0x100] = 13;
                }
                else if (e > -128)
                { // Large numbers map to Infinity
                    shiftTable[i | 0x000] = 24;
                    shiftTable[i | 0x100] = 24;
                }
                else
                { // Infinity and NaN's stay Infinity and NaN's
                    shiftTable[i | 0x000] = 13;
                    shiftTable[i | 0x100] = 13;
                }
            }

            return shiftTable;
        }

        public static unsafe float HalfToSingle(Half half)
        {
            var result = _mantissaTable[_offsetTable[half._value >> 10] + (half._value & 0x3ff)] + _exponentTable[half._value >> 10];
            return *(float*)&result;
        }

        public static unsafe Half SingleToHalf(float single)
        {
            var value = *(uint*)&single;

            var result = (ushort)(_baseTable[(value >> 23) & 0x1ff] + ((value & 0x007fffff) >> _shiftTable[value >> 23]));
            return new Half(result);
        }

        public static Half Negate(Half half) =>
            new Half((ushort)(half._value ^ 0x8000));

        public static Half Abs(Half half) =>
            new Half((ushort)(half._value & 0x7fff));

        public static bool IsNaN(Half half) =>
            (half._value & 0x7fff) > 0x7c00;

        public static bool IsInfinity(Half half) =>
            (half._value & 0x7fff) == 0x7c00;

        public static bool IsPositiveInfinity(Half half) =>
            half._value == 0x7c00;

        public static bool IsNegativeInfinity(Half half) =>
            half._value == 0xfc00;

        public static bool IsNegative(Half half) =>
            (half._value & 0x8000) != 0;

        public static bool IsNormal(Half half) =>
            !IsSubnormal(half) &&
            !IsInfinity(half) &&
            !IsNaN(half);

        public static bool IsSubnormal(Half half) =>
            (half._value & 0x7c00) == 0;
    }
}