using System;
using System.Collections.Generic;

namespace wimm.Missionary
{
    /// <inheritDoc />
    public class ConversionMap<T> : IConversionMap<T>
    {
        private readonly IDictionary<Type, object> _conversions = new Dictionary<Type, object>();

        public void Set<U>(IConversion<T, U> conversion) =>
            _conversions[typeof(U)] = (conversion ?? throw new ArgumentNullException(nameof(conversion)));

        public IConversion<T, U> Get<U>() =>
            _conversions.TryGetValue(typeof(U), out var conversion)
                ? (IConversion<T, U>)conversion
                : null;
    }
}
