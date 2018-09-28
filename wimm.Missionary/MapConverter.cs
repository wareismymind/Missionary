using System;

namespace wimm.Missionary
{
    /// <summary>
    /// An implementation of <see cref="IConverter{T}"/> that uses an
    /// <see cref="IConversionMap{T}"/> to implement its conversions.
    /// </summary>
    public class MapConverter<T> : IConverter<T>
    {
        private readonly IConversionMap<T> _map;

        /// <summary>
        /// Initializes a new <see cref="MapConverter{T}"/>.
        /// </summary>
        /// <param name="map">The map to use for conversions.</param>
        public MapConverter(IConversionMap<T> map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public U To<U>(T from)
        {
            var conversion = _map.Get<U>() ??
                throw new InvalidOperationException($"Conversion to {nameof(U)} not found.");

            if (from == null) throw new ArgumentNullException(nameof(from));

            try
            {
                return conversion.Convert(from);
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Failed to convert {nameof(T)} \"{from}\" to {nameof(U)}.", ex);
            }
        }
    }
}
