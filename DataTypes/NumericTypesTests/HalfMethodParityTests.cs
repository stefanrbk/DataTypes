using NUnit.Framework;

using static System.Half;

namespace System.Tests
{
    [TestFixture(TestOf = typeof(Half))]
    public static class HalfMethodParityTests
    {
        [Test]
        public static void CompareToValidTest(
                               [Range(0, 3)] int option)
        {
            var actual = option switch
            {
                0 => ((Half)1).CompareTo(2),
                1 => ((Half)666).CompareTo(555),
                2 => ((Half)7).CompareTo(7),
                _ => ((Half)7).CompareTo((object)(Half)7)
            };
            var expected = option switch
            {
                0 => 1f.CompareTo(2),
                1 => 666f.CompareTo(555),
                2 => 7f.CompareTo(7),
                _ => 7f.CompareTo((object)7f)
            };
            (var testLeft, var testMid, var testRight) = option switch
            {
                0 => ("1", "", "2"),
                1 => ("666", "", "555"),
                2 => ("7", "", "7"),
                _ => ("7", "(object)", "7")
            };
            TestContext.WriteLine($"(Half){testLeft}.CompareTo({testMid}(Half){testRight})\n" +
                                  $"(Single){testLeft}.CompareTo({testMid}(Single){testRight})\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void CompareToInvalidTest()
        {
            var expected = Assert.Catch(typeof(ArgumentException),
                                        () => 1f.CompareTo(new object()));
            var actual = Assert.Catch(typeof(ArgumentException),
                                      () => ((Half)1).CompareTo(new object()));
            TestContext.WriteLine($"(Half).CompareTo(object)\n" +
                                  $"(Single).CompareTo(object)\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");
        }

        [Test]
        public static void EqualsTest(
                               [Range(0, 3)] int option)
        {
            var actual = option switch
                { 
                    0 => MinValue.Equals(MinValue),
                    1 => ((Half)12345).Equals(12345),
                    _ => new Half().Equals(new float())
                };
            var expected = option switch
            {
                0 => ((float)MinValue).Equals(MinValue),
                1 => ((float)12345).Equals(12345),
                _ => new float().Equals(new double())
            };
            var test = option switch
            {
                0 => $"{MinValue}",
                1 => $"12345",
                _ => $"new "
            };
            var halfTest = $"{test}{(option != 0 && option != 1 ? "Half()" : "")}";
            var singleTest = $"{test}{(option != 0 && option != 1 ? "Single()" : "")}";
            var doubleTest = $"{test}{(option != 0 && option != 1 ? "Double()" : "")}";
            TestContext.WriteLine($"(Half){halfTest}.Equals({singleTest}) == " +
                                  $"(Single){singleTest}.Equals({doubleTest});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        // Not a parity test
        [Test]
        public static void GetBytesTest()
        {
            byte[] expected = { 0x22, 0x33 };
            var value = (Half)0.2229004f;
            var actual = GetBytes(value);

            TestContext.WriteLine($"Half.GetBytes({value});\n" +
                                  $"Expected: '{expected}'\n" +
                                  $"Actual: '{actual}'");

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public static void GetHashCodeTest(
                               [Random(1)] ushort value)
        {
            var target = ToHalf(BitConverter.GetBytes(value), 0);
            var expected = HashCode.Combine(value, 5);
            var actual = target.GetHashCode();

            TestContext.WriteLine($"({target}).GetHashCode();\n" +
                                  $"Expected: '{expected}'\n" +
                                  $"Actual: '{actual}'");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void IsFiniteTest(
                               [Range(0, 3)] int option)
        {
            var actual = option switch
            {
                0 => IsFinite(72),
                1 => IsFinite(PositiveInfinity),
                2 => IsFinite(NegativeInfinity),
                _ => IsFinite(NaN)
            };
            var expected = option switch
            {
                0 => Single.IsFinite((Half)72),
                1 => Single.IsFinite(PositiveInfinity),
                2 => Single.IsFinite(NegativeInfinity),
                _ => Single.IsFinite(NaN)
            };
            var test = option switch
            {
                0 => "72",
                1 => "+∞",
                2 => "-∞",
                _ => "NaN"
            };

            TestContext.WriteLine($"Half.IsFinite({test}) == Single.IsFinite({test});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void IsInfinityTest(
                               [Range(0, 3)] int option)
        {
            var actual = option switch
            {
                0 => IsInfinity(72),
                1 => IsInfinity(PositiveInfinity),
                2 => IsInfinity(NegativeInfinity),
                _ => IsInfinity(NaN)
            };
            var expected = option switch
            {
                0 => Single.IsInfinity((Half)72),
                1 => Single.IsInfinity(PositiveInfinity),
                2 => Single.IsInfinity(NegativeInfinity),
                _ => Single.IsInfinity(NaN)
            };
            var test = option switch
            {
                0 => "72",
                1 => "+∞",
                2 => "-∞",
                _ => "NaN"
            };

            TestContext.WriteLine($"Half.IsInfinity({test}) == Single.IsInfinity({test});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}