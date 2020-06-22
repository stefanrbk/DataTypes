using NUnit.Framework;

using System.Globalization;
using System.Linq;

using static System.Half;

namespace System.Tests
{
    [TestFixture(TestOf = typeof(Half))]
    public static class HalfParityWithSingle
    {
        public static readonly (Half, float)[] ValueTarget =
        {
            (72, 72),
            (PositiveInfinity, PositiveInfinity),
            (NegativeInfinity, NegativeInfinity),
            (NaN, NaN),
            (-42, -42),
            (Epsilon, Single.Epsilon)
        };
        public static readonly Half[] ToStringTargets =
        {
            MathH.PI,
            10000,
            (Half)(-5.5)
        };
        public static readonly string[] FormatTargets =
        {
            "C",
            "e",
            "n",
            "##;(##);[  ##  ]"
        };
        public static readonly IFormatProvider[] ProviderTargets =
        {
            new CultureInfo("en-US"),
            new CultureInfo("fr-FR"),
            new CultureInfo("en-GB"),
            new CultureInfo("ja-JP")
        };
        public static readonly (Half, Half)[] CompareToHalfTargets =
        {
            (1, 2),
            (666,555),
            (7,7)
        };
        public static readonly (Half, object, object)[] CompareToObjectTargets =
        {
            (1, (Half)2f, 2f),
            (666, (Half)555f, 555f),
            (7, (Half)7f, 7f)
        };
        public static readonly (string, bool)[] TryParseTargets =
        {
            ("z", false),
            ("42", true),
            ((-0.5f).ToString(), true)
        };

        [Test]
        public static void compare_to(
                               [ValueSource("CompareToHalfTargets")] (Half left, Half right) target)
        {
            var actual = target.left.CompareTo(target.right);
            var expected = ((float)target.left).CompareTo(target.right);
            (var testLeft, var testRight) = (target.left.ToString(), target.right.ToString());

            TestContext.WriteLine($"(Half){testLeft}.CompareTo((Half){testRight})\n" +
                                  $"(Single){testLeft}.CompareTo((Single){testRight})\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void compare_to(
                               [ValueSource("CompareToObjectTargets")] (Half left, object halfRight, object singleRight) target)
        {
            var actual = target.left.CompareTo(target.halfRight);
            var expected = ((float)target.left).CompareTo(target.singleRight);
            (var testLeft, var testRight) = (target.left.ToString(), target.halfRight.ToString());

            TestContext.WriteLine($"(Half){testLeft}.CompareTo((Object){testRight})\n" +
                                  $"(Single){testLeft}.CompareTo((Object){testRight})\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void compare_to()
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
        public static void equals(
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
            TestContext.WriteLine($"(Half){halfTest}.Equals({singleTest})\n" +
                                  $"(Single){singleTest}.Equals({doubleTest});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_finite(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsFinite(target.half);
            var expected = Single.IsFinite(target.single);

            TestContext.WriteLine($"Half.IsFinite({target.half})\n" +
                                  $"Single.IsFinite({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_infinity(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsInfinity(target.half);
            var expected = Single.IsInfinity(target.single);

            TestContext.WriteLine($"Half.IsInfinity({target.half})\n" +
                                  $"Single.IsInfinity({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_nan(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsNaN(target.half);
            var expected = Single.IsNaN(target.single);

            TestContext.WriteLine($"Half.IsNaN({target.half})\n" +
                                  $"Single.IsNaN({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_negative(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsNegative(target.half);
            var expected = Single.IsNegative(target.single);

            TestContext.WriteLine($"Half.IsNegative({target.half})\n" +
                                  $"Single.IsNegative({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_negative_infinity(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsNegativeInfinity(target.half);
            var expected = Single.IsNegativeInfinity(target.single);

            TestContext.WriteLine($"Half.IsNegativeInfinity({target.half})\n" +
                                  $"Single.IsNegativeInfinity({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_normal(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsNormal(target.half);
            var expected = Single.IsNormal(target.single);

            TestContext.WriteLine($"Half.IsNormal({target.half})\n" +
                                  $"Single.IsNormal({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_positive_infinity(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsPositiveInfinity(target.half);
            var expected = Single.IsPositiveInfinity(target.single);

            TestContext.WriteLine($"Half.IsPositiveInfinity({target.half})\n" +
                                  $"Single.IsPositiveInfinity({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void is_subnormal(
                               [ValueSource("ValueTarget")] (Half half, float single) target)
        {
            var actual = IsSubnormal(target.half);
            var expected = Single.IsSubnormal(target.single);

            TestContext.WriteLine($"Half.IsSubnormal({target.half})\n" +
                                  $"Single.IsSubnormal({target.single});\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void parse(
                               [ValueSource("parse_targets")] string target)
        {
            var actual = Parse(target);
            var expected = Single.Parse(target);

            TestContext.WriteLine($"Half.Parse(\"{target}\") == {actual}\n" +
                                  $"Single.Parse(\"{target}\") == {expected}");

            Assert.That(actual, Is.EqualTo((Half)expected));

            actual = Parse(target, NumberStyles.Number);
            expected = Single.Parse(target, NumberStyles.Number);

            TestContext.WriteLine($"Half.Parse({target}, {NumberStyles.Number})\n" +
                                  $"Single.Parse({target}, {NumberStyles.Number})\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo((Half)expected));
        }

        [Test, Pairwise]
        public static void parse(
                               [ValueSource("parse_with_provider_targets")] 
                                   (string str, IFormatProvider fp) target)
        {
            var actual = Parse(target.str, target.fp);
            var expected = Single.Parse(target.str, target.fp);

            TestContext.WriteLine($"Half.Parse({target.str}, {target.fp})\n" +
                                  $"Single.Parse({target.str}, {target.fp})\n" +
                                  $"Half: {actual}\n" +
                                  $"Single: {expected}");

            Assert.That(actual, Is.EqualTo((Half)expected));
            
            actual = Parse(target.str, NumberStyles.Number, target.fp);
            expected = Single.Parse(target.str, NumberStyles.Number, target.fp);

            TestContext.WriteLine($"Half.Parse(\"{target.str}\", {NumberStyles.Number}, {target.fp})" +
                                  $" == {actual}\n" +
                                  $"Single.Parse(\"{target.str}\", {NumberStyles.Number}, {target.fp})" +
                                  $" == {expected}");

            Assert.That(actual, Is.EqualTo((Half)expected));
        }

        [Test]
        public static void to_string(
                               [ValueSource("ToStringTargets")] Half target)
        {
            var actual = target.ToString();
            var expected = ((float)target).ToString();

            TestContext.WriteLine($"Half.ToString() == \"{actual}\"" +
                                  $"Single.ToString() == \"{expected}\"");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void to_string(
                               [ValueSource("ToStringTargets")] Half target,
                               [ValueSource("FormatTargets")]   string format)
        {
            var actual = target.ToString(format);
            var expected = ((float)target).ToString(format);

            TestContext.WriteLine($"(Half){target}.ToString({format})" +
                                  $" == \"{actual}\"\n" +
                                  $"(Single){target}.ToString({format})" +
                                  $" == \"{expected}\"");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void to_string(
                               [ValueSource("ToStringTargets")] Half target,
                               [ValueSource("ProviderTargets")] IFormatProvider provider)
        {
            var actual = target.ToString(provider);
            var expected = ((float)target).ToString(provider);

            TestContext.WriteLine($"(Half){target}.ToString({provider})" +
                                  $" == \"{actual}\"\n" +
                                  $"(Single){target}.ToString({provider})" +
                                  $" == \"{expected}\"");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void to_string(
                               [ValueSource("ToStringTargets")] Half target,
                               [ValueSource("FormatTargets")] string format,
                               [ValueSource("ProviderTargets")] IFormatProvider provider)
        {
            var actual = target.ToString(format, provider);
            var expected = ((float)target).ToString(format, provider);

            TestContext.WriteLine($"(Half){target}.ToString(\"{format}\", {provider})" +
                                  $" == \"{actual}\"\n" +
                                  $"(Single){target}.ToString(\"{format}\", {provider})" +
                                  $" == \"{expected}\"");

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public static void try_parse(
                               [ValueSource("TryParseTargets")] (string value, bool result) target)
        {
            var actual = TryParse(target.value, out var halfResult);
            var expected = Single.TryParse(target.value, out var singleResult);

            TestContext.WriteLine($"Half.TryParse(\"{target.value}\", out " +
                                  $"halfResult) == {actual}\n" +
                                  $"Single.TryParse(\"{target.value}\", out " +
                                  $"singleResult) == {expected}\n" +
                                  $"halfResult == {halfResult}\n" +
                                  $"singleResult == {singleResult}\n");

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(halfResult, Is.EqualTo((Half)singleResult));

            actual = TryParse(target.value, NumberStyles.Number, 
                              CultureInfo.CurrentCulture, out halfResult);
            expected = Single.TryParse(target.value, NumberStyles.Number,
                                       CultureInfo.CurrentCulture, out singleResult);

            TestContext.WriteLine($"Half.TryParse(\"{target.value}\", {NumberStyles.Number}, " +
                                  $"{CultureInfo.CurrentCulture}, out halfResult) == {actual}\n" +
                                  $"Single.TryParse(\"{target.value}\", {NumberStyles.Number}, " +
                                  $"{CultureInfo.CurrentCulture}, out singleResult) == {expected}\n" +
                                  $"halfResult == {halfResult}\n" +
                                  $"singleResult == {singleResult}");

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(halfResult, Is.EqualTo((Half)singleResult));
        }
        public static string[] parse_targets() =>
            ToStringTargets
                .Select(a => a.ToString())
                .ToArray();
        public static (string, IFormatProvider)[] parse_with_provider_targets() =>
            ToStringTargets
                .SelectMany(a => ProviderTargets
                                     .Select(b => a.ToString(b)))
                .Zip(ToStringTargets
                         .SelectMany(_ => ProviderTargets))
                .ToArray();
    }
}