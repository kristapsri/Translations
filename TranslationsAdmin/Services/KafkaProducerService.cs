using Confluent.Kafka;
using System.Diagnostics.Contracts;

namespace TranslationsAdmin.Services
{
    public interface IKafkaProducerService
    {
        public void SendLanguageUpdatedLog(string log);
        public Task SendToKafka(string topic, string log);
    }

    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IConfiguration _configuration;

        public KafkaProducerService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendLanguageUpdatedLog(string log)
        {
            string topic = "LANGUAGE-UPDATED";
            Task.Run(() => SendToKafka(topic, log));
        }

        public async Task SendToKafka(string topic, string log)
        {
            var config = new ProducerConfig {
                BootstrapServers = _configuration.GetValue<String>("kafka:server", "localhost:29092"),
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var message = new Message<Null, string> { Value = log };

                    var deliveryReport = await producer.ProduceAsync(topic, message);
                    Console.WriteLine($"Message sent (partition: {deliveryReport.Partition}, offset: {deliveryReport.Offset})");
                }
                catch (ProduceException<Null, string> ex)
                {
                    Console.WriteLine($"Delivery failed: {ex.Error.Reason}");
                }
            }
        }
    }
}
