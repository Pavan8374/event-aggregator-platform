namespace Registration.Api.MessageQueue
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public int ConnectionRetryCount { get; set; }
        public string ConnectionRetryDelayMs { get; set; }
        public string ExchangeName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string DeadLetterExchange { get; set; }
        public string DeadLetterQueue { get; set; }
    }
}
