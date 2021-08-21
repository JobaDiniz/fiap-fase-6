using System;
using System.Collections.Generic;
using System.Linq;

namespace iFood.Reviews
{
    public class Store
    {
        private /*readonly*/ IList<Review> reviews = new List<Review>();

        public Store() { }

        public Store(string name, IEnumerable<Review> reviews)
        {
            this.reviews = reviews.ToList();
            Name = name;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
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
        public Review(Guid id, string userName, Rating rating, DateTime date, string description, string response)
        {
            if (string.IsNullOrEmpty(userName)) throw new ArgumentNullException(nameof(userName));

            Id = id;
            UserName = userName;
            Rating = rating;
            Date = date;
            Description = description;
            Response = response;
        }

        public Review(string userName, Rating rating, DateTime date, string description = null)
            : this(Guid.NewGuid(), userName, rating, date, description, null) { }

        public Guid Id { get; }
        public string UserName { get; }
        public Rating Rating { get; }
        public DateTime Date { get; }
        public string Description { get; }
        public string Response { get; }
    }
}
