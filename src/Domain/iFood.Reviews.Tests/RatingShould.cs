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
    }
}
