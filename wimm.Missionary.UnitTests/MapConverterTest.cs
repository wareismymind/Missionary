using System;
using Moq;
using Xunit;

namespace wimm.Missionary.UnitTests
{
    public class MapConverterTest
    {
        [Fact]
        public void Construct_NullMap_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new MapConverter<int>(null));

            Assert.Equal("map", ex.ParamName);
        }

        [Fact]
        public void Construct_ValidArgs_Constructs()
        {
            var map = new Mock<IConversionMap<int>>().Object;

            var _ = new MapConverter<int>(map);
        }

        [Fact]
        public void To_MapDoesNotContainType_Throws()
        {
            var map = new Mock<IConversionMap<int>>().Object;
            var underTest = new MapConverter<int>(map);

            var ex = Assert.Throws<InvalidOperationException>(() => underTest.To<string>(42));
        }

        [Fact]
        public void To_NullFrom_Throws()
        {
            var conversion = new Mock<IConversion<string, int>>().Object;
            var map = new Mock<IConversionMap<string>>();
            map.Setup(m => m.Get<int>()).Returns(conversion);
            var underTest = new MapConverter<string>(map.Object);

            var ex = Assert.Throws<ArgumentNullException>(() => underTest.To<int>(null));

            Assert.Equal("from", ex.ParamName);
        }

        [Fact]
        public void To_ConversionThrows_ExceptionIsWrapped()
        {
            var expected = new Exception();
            var conversion = new Mock<IConversion<int, string>>();
            conversion.Setup(c => c.Convert(It.IsAny<int>())).Throws(expected);
            var map = new Mock<IConversionMap<int>>();
            map.Setup(m => m.Get<string>()).Returns(conversion.Object);
            var underTest = new MapConverter<int>(map.Object);

            var ex = Assert.Throws<InvalidCastException>(() => underTest.To<string>(42));
            var actual = ex.InnerException;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void To_ConversionWorks_ReturnsValueFromConversion()
        {
            var expected = "expected return value";
            var conversion = new Mock<IConversion<int, string>>();
            conversion.Setup(c => c.Convert(It.IsAny<int>())).Returns(expected);
            var map = new Mock<IConversionMap<int>>();
            map.Setup(m => m.Get<string>()).Returns(conversion.Object);
            var underTest = new MapConverter<int>(map.Object);

            var actual = underTest.To<string>(42);

            Assert.Equal(expected, actual);
        }
    }
}
