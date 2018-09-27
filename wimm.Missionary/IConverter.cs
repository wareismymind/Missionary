using System;

namespace wimm.Missionary
{
    /// <summary>
    /// Performs type conversions from <typeparamref name="T"/> to dynamically specified types.
    /// </summary>
    /// <typeparam name="T">The convert-from type.</typeparam>
    public interface IConverter<T>
    {
        /// <summary>
        /// Converts a value of type <typeparamref name="T"/> to <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <param name="from">The value to convert.</param>
        /// <returns>The converted value.</returns>
        /// <exception cref="InvalidOperationException">
        /// The instance is not configured to support conversions to <typeparamref name="U"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="from"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// The value of <paramref name="from"/> can not be converted to a <typeparamref name="U"/>.
        /// </exception>
        U To<U>(T from);
    }
}
