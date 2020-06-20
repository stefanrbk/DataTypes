using NUnit.Framework;

using static System.Half;

namespace System.Tests
{
    public static class HalfMethodTests
    {
        [TestCase(0, ExpectedResult = -1)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 0)]
        [TestCase(3, ExpectedResult = 0)]
        [TestOf(typeof(Half))]
        public static int CompareToValidTest(int option) => 
            option switch
            {
                0 => ((Half)1).CompareTo(2),
                1 => ((Half)666).CompareTo(555),
                2 => ((Half)7).CompareTo(7),
                _ => ((Half)7).CompareTo((object)(Half)7)
            };

        [Test, TestOf(typeof(Half))]
        public static void CompareToInvalidTest() =>
            Assert.Throws(typeof(ArgumentException), 
                          () => ((Half)1).CompareTo(new object()));

        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = false)]
        [TestOf(typeof(Half))]
        public static bool EqualsTest(int option) =>
            option switch
            {
                0 => MinValue.Equals(MinValue),
                1 => ((Half)12345).Equals(12345),
                _ => new Half().Equals(new float())
            };

        [Test, TestOf(typeof(Half))]
        public static void GetBytesTest()
        {
            byte[] expected = { 0x22, 0x33 };
            var value = (Half)0.2229004f;
            var actual = GetBytes(value);

            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }

        [Test, TestOf(typeof(Half))]
        public static void GetHashCodeTest(
                               [Random(1)] ushort value)
        {
            var target = ToHalf(BitConverter.GetBytes(value), 0);
            var expected = HashCode.Combine(value, 5);
            var actual = target.GetHashCode();

            Assert.AreEqual(expected, actual);
        }

        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = false)]
        [TestCase(2, ExpectedResult = false)]
        [TestCase(3, ExpectedResult = false)]
        [TestOf(typeof(Half))]
        public static bool IsFiniteTest(int option) =>
            option switch
            {
                0 => IsFinite(72),
                1 => IsFinite(PositiveInfinity),
                2 => IsFinite(NegativeInfinity),
                _ => IsFinite(NaN)
            };

        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = true)]
        [TestCase(3, ExpectedResult = false)]
        [TestOf(typeof(Half))]
        public static bool IsInfinityTest(int option) =>
            option switch
            {
                0 => IsInfinity(72),
                1 => IsInfinity(PositiveInfinity),
                2 => IsInfinity(NegativeInfinity),
                _ => IsInfinity(NaN)
            };
    }
}