using System;
using System.Collections.Generic;
using System.Linq;

namespace iFood.Reviews
{
    public class Store
    {
        private IList<Review> reviews = new List<Review>();

        public Store() { }

        public Store(string name, IEnumerable<Review> reviews)
        {
            this.reviews = reviews.ToList();
            Name = name;
            AverageRating = CalculateAverageRating();
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; init; }
        public double AverageRating { get; private set; }
        public IEnumerable<Review> Reviews => reviews;

        public void AddReview(Review review)
        {
            if (review is null) throw new ArgumentNullException(nameof(review));

            reviews.Add(review);
            AverageRating = CalculateAverageRating();
        }

        private double CalculateAverageRating() => reviews.Average(r => r.Rating);
    }
}
