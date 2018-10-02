using System;
using wimm.Secundatives;

namespace wimm.Missionary
{
    /// <summary>
    /// A map of types to conversions. The map's type is the convert-from type for all conversions
    /// in the map, and the keys are the convert-to types for their corresponding values.
    /// </summary>
    /// <typeparam name="T">The convert-from type.</typeparam>
    public interface IConversionMap<T>
    {
        /// <summary>
        /// Gets the conversion from <typeparamref name="T"/> to <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <returns>
        /// The conversion for <typeparamref name="U"/>, or <c>null</c> if the map does not contain
        /// a conversion for the specified type.
        /// </returns>
        Maybe<IConversion<T, U>> Get<U>();

        /// <summary>
        /// Sets the conversion from <typeparamref name="T"/> to <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <param name="conversion">The conversion.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conversion"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// A converion to <typeparamref name="U"/> is already set.
        /// </exception>
        void Set<U>(IConversion<T, U> conversion);
    }
}