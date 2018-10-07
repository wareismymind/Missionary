using System;

namespace wimm.Missionary
{
    /// <summary>
    /// A string converter that supports conversions to types that either; have a constructor that
    /// accepts a single string argument, or have a public static Parse method that returns an
    /// instance of the defining type.
    /// </summary>
    public class FromStringConverter : IConverter<string>
    {
        /// <summary>
        /// Converts a string value to <typeparamref name="U"/> for supported types.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <param name="from">The string to convert.</param>
        /// <returns>The converted value.</returns>
        public U To<U>(string from) =>
            FromStringConversion<U>.Supported()
                ? new FromStringConversion<U>().Convert(from)
                : throw new InvalidOperationException($"Automatic conversion to {nameof(U)} is not supported");
    }
}
