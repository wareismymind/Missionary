using System;
using System.Collections.Generic;
using wimm.Secundatives;

namespace wimm.Missionary
{
    /// <inheritDoc />
    public class ConversionMap<T> : IConversionMap<T>
    {
        private readonly IDictionary<Type, object> _conversions = new Dictionary<Type, object>();

        public void Set<U>(IConversion<T, U> conversion)
        {
            if (conversion == null) throw new ArgumentNullException(nameof(conversion));
            if (Get<U>().Exists)
                throw new ArgumentException($"Conversion to {nameof(U)} already set.", nameof(conversion));
            _conversions[typeof(U)] = conversion;
        }

        public Maybe<IConversion<T, U>> Get<U>() =>
            _conversions.TryGetValue(typeof(U), out var conversion)
                ? new Maybe<IConversion<T, U>>((IConversion<T, U>)conversion)
                : new Maybe<IConversion<T, U>>();
    }
}
