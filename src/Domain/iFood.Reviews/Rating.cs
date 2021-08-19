using System;

namespace iFood.Reviews
{
    public struct Rating
    {
        public readonly static double Minimum = 0d;
        public readonly static double Maximum = 5d;

        private readonly double value;

        public Rating(double value)
        {
            if (value < Minimum || value > Maximum)
                throw new ArgumentOutOfRangeException(nameof(value), $"Rating '{value}' is invalid. Supported values are between {Minimum} and {Maximum}");
            this.value = value;
        }

        public static implicit operator double(Rating rating) => rating.value;
        public static implicit operator Rating(double value) => new(value);
    }
}
