using System;
using System.Collections.Generic;
using System.Linq;

namespace iFood.Reviews
{
    public class Store
    {
        private readonly IList<Review> reviews = new List<Review>();

        public Store()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
        public string Name { get; init; }
        public double AverageRating { get; private set; }
        public IEnumerable<Review> Reviews => reviews;

        public void AddReview(Review review)
        {
            if (review is null) throw new ArgumentNullException(nameof(review));

            reviews.Add(review);
            AverageRating = reviews.Sum(r => r.Rating) / reviews.Count;
        }
    }

    public class Review
    {
        public Review(string userName, double rating, DateTime date, string description = null)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            Id = Guid.NewGuid();
            UserName = userName;
            Rating = rating;
            Date = date;
            Description = description;
        }

        public Guid Id { get; }
        public string UserName { get; }
        public Rating Rating { get; }
        public DateTime Date { get; }
        public string Description { get; }
        public string Response { get; }
    }
}
