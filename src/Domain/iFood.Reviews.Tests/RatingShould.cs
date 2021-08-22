using System;
using Xunit;

namespace iFood.Reviews.Tests
{
    public class RatingShould
    {
        [Fact]
        public void Throw_WhenValueIsGreaterThanMaximum() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rating(Rating.Maximum + 10));

        [Fact]
        public void Throw_WhenValueIsLessThanMinimum() =>
            Assert.Throws<ArgumentOutOfRangeException>(() => new Rating(Rating.Minimum - 10));

        [Fact]
        public void BeEquals_WhenUnderlyingValueIsEqual()
        {
            //arrange
            var left = new Rating(4d);
            var right = new Rating(4d);

            //act
            var result = left.Equals(right);

            //assert
            Assert.True(result);
        }

        [Fact]
        public void NotBeEquals_WhenUnderlyingValueIsNotEqual()
        {
            //arrange
            var left = new Rating(4d);
            var right = new Rating(5d);

            //act
            var result = left.Equals(right);

            //assert
            Assert.False(result);
        }

        [Fact]
        public void BeEquals_WhenUnderlyingValueIsEqual_WhenUsingEqualsOperator()
        {
            //arrange
            var left = new Rating(4d);
            var right = new Rating(4d);

            //act
            var result = left == right;

            //assert
            Assert.True(result);
        }

        [Fact]
        public void NotBeEquals_WhenUnderlyingValueNotIsEqual_WhenUsingEqualsOperator()
        {
            //arrange
            var left = new Rating(4d);
            var right = new Rating(5d);

            //act
            var result = left != right;

            //assert
            Assert.True(result);
        }
    }
}
