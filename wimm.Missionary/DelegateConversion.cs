using System;

namespace wimm.Missionary
{
    /// <summary>
    /// An implementation of <see cref="IConversion{T, U}"/> that uses a construct-time supplied
    /// delegate to perform the conversion.
    /// </summary>
    /// <typeparam name="T">The delegate's argument type.</typeparam>
    /// <typeparam name="U">The delegate's return type.</typeparam>
    public class DelegateConversion<T, U> : IConversion<T, U>
    {
        private Func<T, U> _delegate;

        /// <summary>
        /// Initializes a new <see cref="DelegateConversion{T, U}"/>.
        /// </summary>
        /// <param name="delegate">The conversion delegate.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="delegate"/> is <c>null</c>.
        /// </exception>
        public DelegateConversion(Func<T, U> @delegate)
        {
            _delegate = @delegate ?? throw new ArgumentNullException(nameof(@delegate));
        }

        /// <inheritDoc/>
        public U Convert(T from)
        {
            if (from == null) throw new ArgumentNullException(nameof(from));

            try
            {
                return _delegate.Invoke(from);
            }
            catch (Exception ex)
            {
                // TODO:TS Create InvalidConversionException with from, to, and value members.
                throw new InvalidCastException($"Failed to convert {nameof(T)} \"{from}\" to {nameof(U)}.", ex);
            }
        }
    }
}
