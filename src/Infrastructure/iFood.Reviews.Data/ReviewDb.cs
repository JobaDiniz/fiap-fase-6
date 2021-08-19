using System;

namespace iFood.Reviews.Data
{
    public class ReviewDb
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Rating Rating { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Response { get; set; }
    }
}
