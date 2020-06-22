using M = System.MathF;

namespace System
{
    /// <summary>
    ///
    /// </summary>
    public static class MathH
    {
        #region [ Fields ]

        /// <summary>
        ///
        /// </summary>
        public static Half E =>
            (Half)M.E;

        /// <summary>
        ///
        /// </summary>
        public static Half PI =>
            (Half)M.PI;

        #endregion [ Fields ]

        #region [ Methods ]

        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Half Abs(Half value) =>
            HalfHelper.Abs(value);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Acos(Half x) =>
            (Half)M.Acos(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Acosh(Half x) =>
            (Half)M.Acosh(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Asin(Half x) =>
            (Half)M.Asin(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Asinh(Half x) =>
            (Half)M.Asinh(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Atan(Half x) =>
            (Half)M.Atan(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Half Atan2(Half x,
                                 Half y) =>
            (Half)M.Atan2(y, x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Atanh(Half x) =>
            (Half)M.Atanh(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Cbrt(Half x) =>
            (Half)M.Cbrt(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Ceiling(Half x) =>
            (Half)M.Ceiling(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Cos(Half x) =>
            (Half)M.Cos(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Cosh(Half x) =>
            (Half)M.Cosh(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Exp(Half x) =>
            (Half)M.Exp(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Floor(Half x) =>
            (Half)M.Floor(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Half IEEERemainder(Half x,
                                         Half y) =>
            (Half)M.IEEERemainder(x, y);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Log(Half x) =>
            (Half)M.Log(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Half Log(Half x,
                               Half y) =>
            (Half)M.Log(x, y);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Log10(Half x) =>
            (Half)M.Log10(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Half Max(Half x,
                               Half y) =>
            (Half)M.Max(x, y);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Half Min(Half x,
                               Half y) =>
            (Half)M.Min(x, y);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Half Pow(Half x,
                               Half y) =>
            (Half)M.Pow(x, y);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Half Round(Half x,
                                 MidpointRounding mode) =>
            (Half)M.Round(x, mode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="digits"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static Half Round(Half x,
                                 int digits,
                                 MidpointRounding mode) =>
            (Half)M.Round(x, digits, mode);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Round(Half x) =>
            (Half)M.Round(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static Half Round(Half x,
                                 int digits) =>
            (Half)M.Round(x, digits);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Sign(Half x)
        {
            if (Half.IsNaN(x)) throw new ArithmeticException();

            if (x > 0) return 1;
            if (x == 0) return 0;
            return -1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Sin(Half x) =>
            (Half)M.Sin(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Sinh(Half x) =>
            (Half)M.Sinh(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Sqrt(Half x) =>
            (Half)M.Sqrt(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Tan(Half x) =>
            (Half)M.Tan(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Tanh(Half x) =>
            (Half)M.Tanh(x);

        /// <summary>
        ///
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Half Truncate(Half x) =>
            (Half)M.Truncate(x);

        #endregion [ Methods ]
    }
}