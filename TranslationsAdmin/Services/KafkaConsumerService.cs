using Confluent.Kafka;

namespace TranslationsAdmin.Services
{
    public class KafkaConsumerService : IHostedService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<KafkaConsumerService> _logger;
        private ConsumerConfig _config;
        private Task task;

        public KafkaConsumerService(IConfiguration configuration, ILogger<KafkaConsumerService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _config = new ConsumerConfig
            {
                GroupId = _configuration.GetValue<String>("kafka:groupId", "group"),
                BootstrapServers = _configuration.GetValue<String>("kafka:server", "localhost:29092"),
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            task = Task.Run(() => ExecuteTaskAsync(_config));

            return task;
        }

        private async Task ExecuteTaskAsync(ConsumerConfig config)
        {
            _logger.LogInformation("Consumer kafka started");
            try
            {
                using var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build();

                consumerBuilder.Subscribe("LANGUAGE-UPDATE");
                var cancelToken = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var consumer = consumerBuilder.Consume(cancelToken.Token);
                        _logger.LogInformation($"Language has been update and logged from kafka event {consumer.Message.Value}");
                    }

                }
                catch (OperationCanceledException e)
                {
                    consumerBuilder.Close();
                    _logger.LogError(e.Message);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            task?.Dispose();

            _logger.LogInformation("Consumer kafka stoped");

            return Task.CompletedTask;
        }
    }
}
