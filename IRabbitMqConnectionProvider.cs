using RabbitMQ.Client;

namespace BasicRabbitMQClient
{
    public interface IRabbitMqConnectionProvider
    {
        IConnection GetRabbitMqConnection();
    }
}
