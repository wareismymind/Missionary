using System;
using Xunit;

namespace wimm.Missionary.UnitTests
{
    public class DelegateConversionTest
    {
        [Fact]
        public void Construct_NullDelegate_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = new DelegateConversion<int, string>(null);
            });

            Assert.Equal("delegate", ex.ParamName);
        }

        [Fact]
        public void Construct_ValidArgs_Constructs()
        {
            var _ = new DelegateConversion<int, string>(i => i.ToString());
        }

        [Fact]
        public void Convert_NullFrom_Throws()
        {
            var underTest = new DelegateConversion<string, int>(str => str.Length);

            var ex = Assert.Throws<ArgumentNullException>(() =>
            {
                var _ = underTest.Convert(null);
            });

            Assert.Equal("from", ex.ParamName);
        }

        [Fact]
        public void Convert_DelegateThrows_ExceptionIsWrapped()
        {
            var expected = new Exception();
            var underTest = new DelegateConversion<int, string>(_ => throw expected);

            var ex = Assert.Throws<InvalidCastException>(() =>
            {
                underTest.Convert(42);
            });

            var actual = ex.InnerException;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Convert_NonNullFrom_CallsDelegate()
        {
            var called = false;
            var @delegate = new Func<int, Guid>(from =>
            {
                called = true;
                return Guid.NewGuid();
            });
            var underTest = new DelegateConversion<int, Guid>(@delegate);

            var _ = underTest.Convert(42);

            Assert.True(called);
        }
    }
}
