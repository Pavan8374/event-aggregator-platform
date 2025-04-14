namespace Registration.Api.MessageQueue
{
    public interface IMessageQueueService
    {
        Task PublishAsync<T>(string queueName, T message);
        Task ConsumeAsync<T>(string queueName, Func<T, Task> handler);
    }
}
