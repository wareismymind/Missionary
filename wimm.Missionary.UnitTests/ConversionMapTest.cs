using System;
using Moq;
using Xunit;

namespace wimm.Missionary.UnitTests
{
    public class ConversionMapTest
    {
        [Fact]
        public void Get_MapDoesNotContainConversion_ReturnsNull()
        {
            var underTest = new ConversionMap<int>();

            var actual = underTest.Get<string>();

            Assert.Null(actual);
        }

        [Fact]
        public void Set_NullConversion_Throws()
        {
            var underTest = new ConversionMap<int>();

            var ex = Assert.Throws<ArgumentNullException>(() => underTest.Set<string>(null));

            Assert.Equal("conversion", ex.ParamName);
        }

        [Fact]
        public void Set_MapDoesNotContainConversion_Returns()
        {
            var conversion = new Mock<IConversion<int, string>>();
            var underTest = new ConversionMap<int>();

            underTest.Set(conversion.Object);
        }

        [Fact]
        public void Get_MapContainsConversion_ReturnsConversion()
        {
            var expected = new Mock<IConversion<int, string>>().Object;
            var underTest = new ConversionMap<int>();
            underTest.Set(expected);

            var actual = underTest.Get<string>();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Set_MapContainsConversion_Throws()
        {
            var underTest = new ConversionMap<int>();
            underTest.Set(new Mock<IConversion<int, string>>().Object);

            var ex = Assert.Throws<ArgumentException>(() =>
            {
                underTest.Set(new Mock<IConversion<int, string>>().Object);
            });

            Assert.Equal("conversion", ex.ParamName);
        }
    }
}
