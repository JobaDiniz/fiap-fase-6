using AutoFixture;
using iFood.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace iFood.Reviews.Tests
{
    public class StoreShould
    {
        [ReviewsAutoData]
        [Theory]
        public void Throw_WhenReviewIsNull(Store sut) =>
            Assert.Throws<ArgumentNullException>(() => sut.AddReview(null));

        [ReviewsAutoData]
        [Theory]
        public void CalculateStoreAverage_WhenNewReviewIsAdded(IEnumerable<Review> reviews, IFixture fixture)
        {
            //arrange
            var review = fixture.Create<Review>();
            var expectedAverage = reviews.Concat(new[] { review }).Average(c => c.Rating);
            var sut = new Store(fixture.Create<string>(), reviews);

            //act
            sut.AddReview(review);

            //assert
            Assert.Equal(expectedAverage, sut.AverageRating);
        }
    }
}
