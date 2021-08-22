using System;

namespace iFood.Reviews
{
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
