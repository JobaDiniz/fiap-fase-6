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

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is not Review store) return false;

            return store.Id == Id;
        }

        public override int GetHashCode() => Id.GetHashCode();

        public static bool operator ==(Review left, Review right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Review left, Review right) => !(left == right);
    }
}
