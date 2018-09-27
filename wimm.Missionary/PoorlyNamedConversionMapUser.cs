using System;

namespace wimm.Missionary
{
    /// <summary>
    /// An implementation of <see cref="IPerformConversionsFromTheSameTypeToDifferentTypes{T}"/>
    /// that uses an <see cref="IMapOfTypeToConversionToKeyFrom{T}"/> to implement its conversions.
    /// </summary>
    public class PoorlyNamedConversionMapUser<T> : IPerformConversionsFromTheSameTypeToDifferentTypes<T>
    {
        // TODO:TS Name this better

        private readonly IMapOfTypeToConversionToKeyFrom<T> _map;

        /// <summary>
        /// Initializes a new <see cref="PoorlyNamedConversionMapUser{T}"/>.
        /// </summary>
        /// <param name="map">The map to use for conversions.</param>
        public PoorlyNamedConversionMapUser(IMapOfTypeToConversionToKeyFrom<T> map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public U To<U>(T from)
        {
            var conversion = _map.Get<U>() ??
                throw new InvalidOperationException($"Conversion to {nameof(U)} not found.");

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
