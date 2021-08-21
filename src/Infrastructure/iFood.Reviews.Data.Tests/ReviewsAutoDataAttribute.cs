using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using iFood.Reviews;

namespace iFood
{
    public class ReviewsAutoDataAttribute : AutoDataAttribute
    {
        public ReviewsAutoDataAttribute()
            : base(CreateFixture) { }

        private static IFixture CreateFixture()
        {
            var fixture = new Fixture()
                 .Customize(new AutoMoqCustomization())
                 .Customize(new RatingCustomization());

            fixture.Customize<Store>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
            fixture.Customize<Review>(c => c.FromFactory(new MethodInvoker(new GreedyConstructorQuery())));
            return fixture;
        }
    }
}
