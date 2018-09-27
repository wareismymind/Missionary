namespace wimm.Missionary
{
    /// <summary>
    /// A map of types to conversions. The map's type is the convert-from type for all conversions
    /// in the map, and the keys are the convert-to types for their corresponding values.
    /// </summary>
    /// <typeparam name="T">The convert-from type.</typeparam>
    public interface IConversionMap<T>
    {
        // TODO:TS Consider using a maybe instead of null

        /// <summary>
        /// Gets the conversion from <typeparamref name="T"/> to <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <returns>
        /// The conversion for <typeparamref name="U"/>, or <c>null</c> if the map does not contain
        /// a conversion for the specified type.
        /// </returns>
        IConversion<T, U> Get<U>();

        // TODO: Consider throwing if the same typed conversion is set more than once.

        /// <summary>
        /// Sets the conversion from <typeparamref name="T"/> to <typeparamref name="U"/>. Existing
        /// conversions will be replaced if the same type is set more than once.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <param name="conversion">The conversion.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conversion"/> is <c>null</c>.
        /// </exception>
        void Set<U>(IConversion<T, U> conversion);
    }
}