namespace wimm.Missionary
{
    /// <summary>
    /// A map of types to conversions from <typeparamref name="T"/> to the type.
    /// </summary>
    /// <typeparam name="T">The convert-from <typeparamref name="T"/>.</typeparam>
    public interface IMapOfTypeToConversionToKeyFrom<T>
    {
        // TODO:TS Name this better

        /// <summary>
        /// Gets the conversion from <typeparamref name="T"/> to <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <returns>
        /// The conversion if it is set for <typeparamref name="U"/>, otherwise <c>null</c>.
        /// </returns>
        IConversion<T, U> Get<U>();

        /// <summary>
        /// Sets the conversion from <typeparamref name="T"/> to <typeparamref name="U"/>.
        /// </summary>
        /// <typeparam name="U">The convert-to type.</typeparam>
        /// <param name="conversion">The conversion.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="conversion"/> is <c>null</c>.
        /// </exception>
        void Set<U>(IConversion<T, U> conversion);
    }
}