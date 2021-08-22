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

        public void AddReviews(params Review[] reviews)
        {
            if (reviews is null) throw new ArgumentNullException(nameof(reviews));

            foreach (var review in reviews)
                this.reviews.Add(review);

            AverageRating = CalculateAverageRating();
        }

        private double CalculateAverageRating() => reviews.Average(r => r.Rating);

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is not Store store) return false;

            return store.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Store left, Store right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Store left, Store right) => !(left == right);
    }
}
