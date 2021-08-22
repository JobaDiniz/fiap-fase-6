using AutoFixture;
using AutoFixture.Kernel;
using iFood.Reviews;
using System.Reflection;

namespace iFood.Tests
{
    public class RatingCustomization : ICustomization
    {
        void ICustomization.Customize(IFixture fixture) => fixture.Customizations.Add(new RatingBuilder());

        class RatingBuilder : ISpecimenBuilder
        {
            public object Create(object request, ISpecimenContext context)
            {
                var property = request as PropertyInfo;
                var parameter = request as ParameterInfo;
                var type = property?.PropertyType ?? parameter?.ParameterType;

                if (type == null ||
                    type != typeof(Rating))
                    return new NoSpecimen();

                var value = context.Resolve(new RangedNumberRequest(typeof(double), Rating.Minimum, Rating.Maximum));
                return new Rating((double)value);
            }
        }
    }
}
