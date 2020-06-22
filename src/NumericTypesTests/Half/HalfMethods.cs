using NUnit.Framework;

using static System.Half;

namespace System.Tests
{
    [TestFixture]
    public static class HalfMethods
    {
        [Test]
        public static void compare_to()
        {
            // CompareTo(object)

            Half a = 42;
            object b = (Half)50;

            if (a.CompareTo(b) < 0)
                Console.WriteLine($"{a} is less than {b}.");
            if (a.CompareTo(b) > 0)
                Console.WriteLine($"{a} is greater than {b}.");
            if (a.CompareTo(b) == 0)
                Console.WriteLine($"{a} equals {b}.");

            Assert.That(a.CompareTo(b), Is.LessThan(0));
        }
        [Test]
        public static void get_bytes()
        {
            byte[] expected = { 0x22, 0x33 };
            var value = (Half)0.2229004f;
            var actual = GetBytes(value);

            TestContext.WriteLine($"Half.GetBytes({value}) == {actual}\n" +
                                  $"Expected: '{expected}");

            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public static void get_hash_code()
        {
            var value = TestContext.CurrentContext.Random.NextUShort();
            var target = ToHalf(BitConverter.GetBytes(value), 0);
            var expected = HashCode.Combine(value, 5);
            var actual = target.GetHashCode();

            TestContext.WriteLine($"({target}).GetHashCode() == {actual}\n" +
                                  $"Expected: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void get_type_code()
        {
            var target = (Half)TestContext.CurrentContext.Random.NextUShort();
            var expected = (TypeCode)255;
            var actual = target.GetTypeCode();

            TestContext.WriteLine($"{target}.GetTypeCode() == {actual}\n" +
                                  $"Expected: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void op_exponent()
        {
            var leftTarget = (Half)3;
            var rightTarget = (Half)4;
            var expected = (Half)81;
            var actual = (Half)op_Exponent(leftTarget, rightTarget);

            TestContext.WriteLine($"{leftTarget} ^ {rightTarget} == {actual}\n" +
                                  $"Expected: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void to_half()
        {
            byte[] target = { 0x80, 0x46 };
            var expected = (Half)6.5;
            var actual = ToHalf(target, 0);

            TestContext.WriteLine($"Half.ToHalf({target}, 0) == {actual}\n" +
                                  $"Expected: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
