using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Registration.Api.MessageQueue
{
    public class RabbitMQService : IMessageQueueService, IDisposable
    {
        private readonly ILogger<RabbitMQService> _logger;
        private readonly RabbitMQSettings _rabbitMQSettings;
        private IConnection _connection;
        private IChannel _channel;
        private readonly SemaphoreSlim _initLock = new SemaphoreSlim(1, 1);
        private bool _initialized;

        public RabbitMQService(ILogger<RabbitMQService> logger, IOptions<RabbitMQSettings> rabbitMQSettings)
        {
            _logger = logger;
            _rabbitMQSettings = rabbitMQSettings.Value;
        }

        private async Task InitializeConnectionAsync()
        {
            if (_initialized) return;

            try
            {
                await _initLock.WaitAsync();

                if (_initialized) return;

                var factory = new ConnectionFactory()
                {
                    HostName = _rabbitMQSettings.Host ?? "localhost",
                    Port = 5672, // Set directly since you're explicitly setting it
                    UserName = _rabbitMQSettings.UserName ?? "guest",
                    Password = _rabbitMQSettings.Password ?? "guest",
                };

                _connection = await factory.CreateConnectionAsync();
                _channel = await _connection.CreateChannelAsync();
                _initialized = true;
            }
            finally
            {
                _initLock.Release();
            }
        }

        public async Task PublishAsync<T>(string queueName, T message)
        {
            try
            {
                await InitializeConnectionAsync();

                await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);
                var serializedMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(serializedMessage);

                var props = new BasicProperties
                {
                    ContentType = "text/plain",
                    DeliveryMode = (DeliveryModes)2,
                    Expiration = "36000000"
                };

                await _channel.BasicPublishAsync(
                    exchange: "",
                    routingKey: queueName,
                    mandatory: true,
                    basicProperties: props,
                    body: body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing message to queue {QueueName}", queueName);
                throw;
            }
        }

        public async Task ConsumeAsync<T>(string queueName, Func<T, Task> handler)
        {
            try
            {
                await InitializeConnectionAsync();

                await _channel.QueueDeclareAsync(queueName, durable: true, exclusive: false, autoDelete: false);

                var consumer = new AsyncEventingBasicConsumer(_channel);
                consumer.ReceivedAsync += async (_, ea) =>
                {
                    try
                    {
                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var deserializedMessage = JsonSerializer.Deserialize<T>(message);

                        if (deserializedMessage != null)
                        {
                            await handler(deserializedMessage);
                            await _channel.BasicAckAsync(ea.DeliveryTag, false);
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error processing message from queue {QueueName}", queueName);
                        await _channel.BasicNackAsync(ea.DeliveryTag, false, true);
                    }
                };

                await _channel.BasicConsumeAsync(queue: queueName, autoAck: false, consumer: consumer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error setting up consumer for queue {QueueName}", queueName);
                throw;
            }
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            _initLock.Dispose();
        }
    }
}
