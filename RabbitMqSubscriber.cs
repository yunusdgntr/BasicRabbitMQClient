using Microsoft.Extensions.Logging;
using System;
using RabbitMQ.Client;

namespace BasicRabbitMQClient
{
    public class RabbitMqSubscriber
    {

        private readonly IRabbitMqConnectionProvider _connectionProvider;
        private readonly string _queueName;
        private readonly string _exchangeName;

        /// <summary>
        /// RabbitMQ ya yeni kuyruk tanımlama ve exhange e bind etme
        /// </summary>
        /// <param name="connectionProvider">Bağlantı bilgilerini içeren ConnectionProvider türünden paremetre</param>
        /// <param name="exchangeName"></param>
        /// <param name="queueName"></param>     
        public RabbitMqSubscriber(IRabbitMqConnectionProvider connectionProvider, string exchangeName, string queueName)
        {
            _connectionProvider = connectionProvider;
            _exchangeName = exchangeName;
            _queueName = queueName;

        }

        /// <summary>
        /// Mesajı ilgili kuyruğa ekle
        /// </summary>
        public bool Subscribe()
        {
            try
            {
                using (var connection = _connectionProvider.GetRabbitMqConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var basicProperties = channel.CreateBasicProperties();
                        basicProperties.Persistent = true;
                        channel.QueueDeclare(_exchangeName + "." + _queueName, true, false, false, null);
                        channel.QueueBind(_exchangeName + "." + _queueName, _exchangeName, "");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
