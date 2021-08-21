using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using iFood.Reviews;
using iFood.Reviews.Data;
using System;

namespace iFood
{
    public class MockableAutoDataAttribute : AutoDataAttribute
    {
        public MockableAutoDataAttribute()
            : base(CreateFixture) { }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture()
                 .Customize(new AutoMoqCustomization())
                 .Customize(new RatingCustomization());

            fixture.Customize<ReviewDb>(x => x.With(p => p.Rating, () => new Random().Next((int)Rating.Minimum, (int)Rating.Maximum)));
            return fixture;
        }
    }
}
