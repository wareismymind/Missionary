using System;
using System.Reflection;
using wimm.Secundatives;

namespace wimm.Missionary
{
    /// <summary>
    /// A string conversion that's implemented by using either a constructor that accepts a single
    /// string argument, or a public static Parse method to obtain the converted value. A type must
    /// have at least of these methods available to be supported.
    /// </summary>
    /// <inheritDoc />
    public class FromStringConversion<T> : IConversion<string, T>
    {
        private readonly DelegateConversion<string, T> _conversion;

        /// <summary>
        /// Indicates whether or not a type is supported.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <returns>
        /// <c>true</c> is <typeparamref name="T"/> is supported, otherwise <c>false</c>.
        /// </returns>
        public static bool Supported() => GetConversion().Exists;

        /// <summary>
        /// Initializes a new new <see cref="FromStringConversion{T}"/>.
        /// </summary>
        /// <exception cref="NotSupportedException">
        /// <typeparamref name="T"/> has neither a constructor that accepts a single string
        /// argument, nor a public static method called Parse that returns an instance of
        /// <typeparamref name="T"/>.
        /// </exception>
        public FromStringConversion()
        {
            var conversion = GetConversion();

            if (!conversion.Exists)
                throw new NotSupportedException($"{nameof(T)} does not support automatic conversion from string.");

            _conversion = new DelegateConversion<string, T>(conversion.Value);
        }

        public T Convert(string from) => _conversion.Convert(from);

        private static Maybe<Func<string, T>> GetConversion()
        {
            var constructor = typeof(T).GetConstructor(new[] { typeof(string) });
            if (constructor != null)
                return new Maybe<Func<string, T>>(str => (T)constructor.Invoke(new[] { str }));

            var name = "Parse";
            var bindingFlags = BindingFlags.Static | BindingFlags.Public;
            var types = new[] { typeof(string) };
            var parseMethod = typeof(T).GetMethod(name, bindingFlags, null, types, null);
            if (parseMethod != null && parseMethod.ReturnType == typeof(T))
                return new Maybe<Func<string, T>>(str => (T)parseMethod.Invoke(null, new[] { str }));

            return new Maybe<Func<string, T>>();
        }
    }
}
