using System;
using Xunit;
using Homework.First;

namespace HomeWorkTests
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(5, "B")]
        [InlineData(5, "A")]
        [InlineData(5, "A", "B")]
        [InlineData(7, "A", "B", "B")]
        [InlineData(29, "A", "A", "B", "A", "B", "B", "A", "B")]
        [InlineData(29, "A", "A", "A", "A", "B", "B", "B", "B")]
        [InlineData(49, "B", "B", "B", "B", "A", "A", "A", "A")]
        public void CanAddTheory(int expected, params string[] args)
        {
            // given
            var sim = new Simulator();
            var productionList = string.Join(string.Empty, args);

            // whem
            var result = sim.Simulate(productionList);

            // Then
            Assert.Equal(expected, result);
        }
    }
}
