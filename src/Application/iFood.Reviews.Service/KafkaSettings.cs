using Confluent.Kafka;

namespace iFood.Reviews.Service
{
    public class KafkaSettings
    {
        public string Topic { get; set; }
        public ConsumerConfig Consumer { get; set; }
    }
}
