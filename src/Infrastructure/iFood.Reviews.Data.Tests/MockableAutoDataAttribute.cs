using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace iFood
{
    public class MockableAutoDataAttribute : AutoDataAttribute
    {
        public MockableAutoDataAttribute()
            : base(CreateFixture) { }

        private static IFixture CreateFixture()
            => new Fixture().Customize(new AutoMoqCustomization());
    }
}
