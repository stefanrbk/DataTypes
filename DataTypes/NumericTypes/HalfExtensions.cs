using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System
{
    /// <summary>
    ///
    /// </summary>
    public static class HalfExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static Half ReadHalf(this BinaryReader reader)
        {
            if (reader is null)
                throw new ArgumentNullException(nameof(reader));
            return Half.ToHalf(reader.ReadBytes(2), 0);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void Write(this BinaryWriter writer, Half value)
        {
            if (writer is null)
                throw new ArgumentNullException(nameof(writer));
            writer.Write(Half.GetBytes(value));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void Write(this TextWriter writer, Half value)
        {
            if (writer is null)
                throw new ArgumentNullException(nameof(writer));
            writer.Write(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        public static void WriteLine(this TextWriter writer, Half value)
        {
            if (writer is null)
                throw new ArgumentNullException(nameof(writer));
            writer.WriteLine(value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half Average(this IEnumerable<Half> source)
        {
            if (source is null) throw new ArgumentNullException();
            var count = 0;
            var total = (Half)0f;
            foreach (var value in source)
            {
                total += value;
                count++;
            }
            if (count == 0) throw new InvalidOperationException();
            return total / count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half? Average(this IEnumerable<Half?> source)
        {
            if (source is null) throw new ArgumentNullException();

            var count = 0;
            var total = (Half)0;
            foreach (var value in source)
            {
                if (!value.HasValue) continue;

                total += value.Value;
                count++;
            }
            if (count == 0) return null;
            return total / count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half Average<TSource>(this IEnumerable<TSource> source,
                                            Func<TSource, Half> selector)
        {
            if (source is null || selector is null)
                throw new ArgumentNullException();

            var count = 0;
            var total = (Half)0f;
            foreach (var value in source)
            {
                total += selector(value);
                count++;
            }
            if (count == 0) throw new InvalidOperationException();
            return total / count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half? Average<TSource>(this IEnumerable<TSource> source,
                                             Func<TSource, Half?> selector)
        {
            if (source is null || selector is null)
                throw new ArgumentNullException();

            var count = 0;
            var total = (Half)0;
            foreach (var tValue in source)
            {
                var value = selector(tValue);
                if (!value.HasValue) continue;

                total += value.Value;
                count++;
            }
            if (count == 0) return null;
            return total / count;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half Max(this IEnumerable<Half> source)
        {
            if (source is null) throw new ArgumentNullException();
            var result = (Half?)null;
            foreach (var value in source)
                if (!result.HasValue ||
                    result < value)
                    result = value;

            if (result.HasValue) return result.Value;
            throw new InvalidOperationException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half? Max(this IEnumerable<Half?> source)
        {
            if (source is null) throw new ArgumentNullException();

            var result = (Half?)null;
            foreach (var value in source)
                if (!result.HasValue ||
                    (value.HasValue &&
                     result < value))
                    result = value;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half Max<TSource>(this IEnumerable<TSource> source,
                                        Func<TSource, Half> selector)
        {
            if (source is null || selector is null)
                throw new ArgumentNullException();

            var result = (Half?)null;
            foreach (var value in source)
            {
                var selection = selector(value);
                if (!result.HasValue ||
                    result < selection)
                    result = selection;
            }

            if (result.HasValue) return result.Value;
            throw new InvalidOperationException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half? Max<TSource>(this IEnumerable<TSource> source,
                                        Func<TSource, Half?> selector)
        {
            if (source is null || selector is null)
                throw new ArgumentNullException();

            var result = (Half?)null;
            foreach (var value in source)
            {
                var selection = selector(value);
                if (!result.HasValue ||
                    (selection.HasValue &&
                     result < selection))
                    result = selection;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half Min(this IEnumerable<Half> source)
        {
            if (source is null) throw new ArgumentNullException();
            var result = (Half?)null;
            foreach (var value in source)
                if (!result.HasValue ||
                    result > value)
                    result = value;

            if (result.HasValue) return result.Value;
            throw new InvalidOperationException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half? Min(this IEnumerable<Half?> source)
        {
            if (source is null) throw new ArgumentNullException();

            var result = (Half?)null;
            foreach (var value in source)
                if (!result.HasValue ||
                    (value.HasValue &&
                     result > value))
                    result = value;

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half Min<TSource>(this IEnumerable<TSource> source,
                                        Func<TSource, Half> selector)
        {
            if (source is null || selector is null)
                throw new ArgumentNullException();

            var result = (Half?)null;
            foreach (var value in source)
            {
                var selection = selector(value);
                if (!result.HasValue ||
                    result > selection)
                    result = selection;
            }

            if (result.HasValue) return result.Value;
            throw new InvalidOperationException();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half? Min<TSource>(this IEnumerable<TSource> source,
                                        Func<TSource, Half?> selector)
        {
            if (source is null || selector is null)
                throw new ArgumentNullException();

            var result = (Half?)null;
            foreach (var value in source)
            {
                var selection = selector(value);
                if (!result.HasValue ||
                    (selection.HasValue &&
                     result > selection))
                    result = selection;
            }

            return result;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half Sum(this IEnumerable<Half> source) =>
            source.Aggregate((result, value) => result += value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Half? Sum(this IEnumerable<Half?> source) =>
            source.Aggregate((result, value) =>
                (result + value) ?? result ?? value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half Sum<TSource>(this IEnumerable<TSource> source,
                                        Func<TSource, Half> selector) =>
            source.Aggregate((Half)0, (result, value) =>
                result += selector(value));

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Half? Sum<TSource>(this IEnumerable<TSource> source,
                                         Func<TSource, Half?> selector) =>
            source.Aggregate((Half?)0, (result, value) =>
                {
                    var selection = selector(value);
                    return (result + selection) ?? result ?? selection;
                });
    }
}