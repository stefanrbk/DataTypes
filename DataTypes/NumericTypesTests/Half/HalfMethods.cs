using NUnit.Framework;

using static System.Half;

namespace System.Tests
{
    [TestFixture]
    public static class HalfMethods
    {
        [Test]
        public static void get_bytes()
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
        public static void get_hash_code()
        {
            var value = TestContext.CurrentContext.Random.NextUShort();
            var target = ToHalf(BitConverter.GetBytes(value), 0);
            var expected = HashCode.Combine(value, 5);
            var actual = target.GetHashCode();

            TestContext.WriteLine($"({target}).GetHashCode();\n" +
                                  $"Expected: '{expected}'\n" +
                                  $"Actual: '{actual}'");

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
