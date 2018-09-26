using System;

namespace wimm.Missionary
{
    /// <summary>
    /// A mechanism for converting a value of type <typeparamref name="T"/> to a value of type
    /// <typeparamref name="U"/>.
    /// </summary>
    /// <typeparam name="T">The convert-from type.</typeparam>
    /// <typeparam name="U">The convert-to type.</typeparam>
    public interface IConversion<T, U>
    {
        /// <summary>
        /// Converts <paramref name="from"/> to a value of type <typeparamref name="U"/>.
        /// </summary>
        /// <param name="from"></param>
        /// <returns>
        /// A <typeparamref name="U"/> whose value was derived from <paramref name="from"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="from"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidCastException">
        /// The value of <paramref name="from"/> does not represent a valid <typeparamref name="U"/>.
        /// </exception>
        U Convert(T from);
    }
}
