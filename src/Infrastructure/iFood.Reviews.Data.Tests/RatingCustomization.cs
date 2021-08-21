using AutoFixture;
using AutoFixture.Kernel;
using iFood.Reviews;
using System.Reflection;

namespace iFood
{
    public class RatingCustomization : ICustomization
    {
        void ICustomization.Customize(IFixture fixture) => fixture.Customizations.Add(new RatingBuilder());

        class RatingBuilder : ISpecimenBuilder
        {
            public object Create(object request, ISpecimenContext context)
            {
                var property = request as PropertyInfo;
                if (property == null ||
                    property.PropertyType != typeof(Rating))
                    return new NoSpecimen();

                var value = context.Resolve(new RangedNumberRequest(typeof(double), Rating.Minimum, Rating.Maximum));
                return new Rating((double)value);
            }
        }
    }
}
