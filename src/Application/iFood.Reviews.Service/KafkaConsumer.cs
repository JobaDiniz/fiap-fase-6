using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry.Serdes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace iFood.Reviews.Service
{
    public class KafkaConsumer : BackgroundService
    {
        private readonly ILogger<KafkaConsumer> logger;
        private readonly KafkaSettings settings;
        private readonly IStoreRepository storeRepository;
        private readonly IServiceScope scope;

        public KafkaConsumer(ILogger<KafkaConsumer> logger, KafkaSettings settings, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.settings = settings;
            scope = serviceProvider.CreateScope();
            storeRepository = scope.ServiceProvider.GetRequiredService<IStoreRepository>();
        }

        /// <summary>
        /// This is used for Integration tests only.
        /// Since <see cref="BackgroundService"/> is registered as "Singleton" and <see cref="StoreContext"/> as "Scoped",
        /// we cannot directly received <see cref="IStoreRepository"/> because an error would occur.
        /// <see cref="StoreContext"/> needs to be "Scoped" because it's used in Asp.net Core controllers.
        /// </summary>
        internal KafkaConsumer(ILogger<KafkaConsumer> logger, KafkaSettings settings, IStoreRepository storeRepository)
        {
            this.logger = logger;
            this.settings = settings;
            this.storeRepository = storeRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellation)
        {
            var builder = new ConsumerBuilder<Ignore, StoreCreatedEvent>(settings.Consumer)
                .SetErrorHandler(LogError)
                .SetValueDeserializer(new JsonDeserializer<StoreCreatedEvent>().AsSyncOverAsync());

            using var consumer = builder.Build();
            consumer.Subscribe(settings.Topic);

            var result = consumer.Consume(cancellation);
            var @event = result.Message.Value;

            await storeRepository.Add(@event.Id, @event.Name, cancellation);
        }

        private void LogError(IConsumer<Ignore, StoreCreatedEvent> consumer, Error error)
        {
            var message = $"Error consuming Kafka topic {settings.Topic}: [{error.Code}] {error.Reason}";
            if (error.IsFatal)
                logger.LogError(message);
            else
                logger.LogWarning(message);
        }

        public override void Dispose()
        {
            scope?.Dispose();
            base.Dispose();
        }
    }
}
