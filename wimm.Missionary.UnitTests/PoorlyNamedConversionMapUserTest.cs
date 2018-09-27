using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using Xunit;

namespace wimm.Missionary.UnitTests
{
    public class PoorlyNamedConversionMapUserTest
    {
        [Fact]
        public void Construct_NullMap_Throws()
        {
            var ex = Assert.Throws<ArgumentNullException>(() => new PoorlyNamedConversionMapUser<int>(null));

            Assert.Equal("map", ex.ParamName);
        }

        [Fact]
        public void Construct_ValidArgs_Constructs()
        {
            var map = new Mock<IMapOfTypeToConversionToKeyFrom<int>>().Object;

            var _ = new PoorlyNamedConversionMapUser<int>(map);
        }

        [Fact]
        public void To_MapDoesNotContainType_Throws()
        {
            var map = new Mock<IMapOfTypeToConversionToKeyFrom<int>>().Object;
            var underTest = new PoorlyNamedConversionMapUser<int>(map);

            var ex = Assert.Throws<InvalidOperationException>(() => underTest.To<string>(42));
        }

        [Fact]
        public void To_ConversionThrows_ExceptionIsWrapped()
        {
            var expected = new Exception();
            var conversion = new Mock<IConversion<int, string>>();
            conversion.Setup(c => c.Convert(It.IsAny<int>())).Throws(expected);
            var map = new Mock<IMapOfTypeToConversionToKeyFrom<int>>();
            map.Setup(m => m.Get<string>()).Returns(conversion.Object);
            var underTest = new PoorlyNamedConversionMapUser<int>(map.Object);

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
            var map = new Mock<IMapOfTypeToConversionToKeyFrom<int>>();
            map.Setup(m => m.Get<string>()).Returns(conversion.Object);
            var underTest = new PoorlyNamedConversionMapUser<int>(map.Object);

            var actual = underTest.To<string>(42);

            Assert.Equal(expected, actual);
        }
    }
}
