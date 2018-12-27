using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace BasicRabbitMQClient
{
    public class RabbitMqConnectionProvider : IRabbitMqConnectionProvider
    {
        private readonly string _hostName;
        private readonly string _username;
        private readonly string _password;

        public RabbitMqConnectionProvider(string hostName, string username, string password)
        {
            _hostName = hostName;
            _username = username;
            _password = password;
        }

        public IConnection GetRabbitMqConnection()
        {
            var connectionFactory = new ConnectionFactory()
            {

                HostName = _hostName,
                UserName = _username,
                Password = _password,
            };
            return connectionFactory.CreateConnection();
        }
    }
}
