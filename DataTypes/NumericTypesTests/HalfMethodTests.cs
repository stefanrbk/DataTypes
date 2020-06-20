using NUnit.Framework;

using static System.Half;

namespace System.Tests
{
    public static class HalfMethodTests
    {
        [TestCase(0, ExpectedResult = -1)]
        [TestCase(1, ExpectedResult = 1)]
        [TestCase(2, ExpectedResult = 0)]
        [TestOf(typeof(Half))]
        public static int CompareToValid(int option) => 
            option switch
            {
                0 => ((Half)1).CompareTo(2),
                1 => ((Half)666).CompareTo(555),
                _ => ((Half)7).CompareTo(7)
            };
        [Test, TestOf(typeof(Half))]
        public static void CompareToInvalid() =>
            Assert.Throws(typeof(ArgumentException), 
                          () => ((Half)1).CompareTo(new object()));
        [TestCase(0, ExpectedResult = true)]
        [TestCase(1, ExpectedResult = true)]
        [TestCase(2, ExpectedResult = false)]
        [TestOf(typeof(Half))]
        public static bool Equals(int option) =>
            option switch
            {
                0 => MinValue.Equals(MinValue),
                1 => ((Half)12345).Equals(12345),
                _ => new Half().Equals(new float())
            };
    }
}