using System;

namespace iFood.Reviews.Service
{
    public class StoreCreatedEvent
    {
        public string Name { get; set; }
        public Guid Id { get; set; }
    }
}
