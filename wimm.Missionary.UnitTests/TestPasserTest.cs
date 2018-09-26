using System;
using Xunit;

namespace wimm.Missionary.UnitTests
{
    public class TestPasserTest
    {
        [Fact]
        public void Test()
        {
            var underTest = new TestPasser();

            Assert.True(underTest.PassesTest());
        }
    }
}
