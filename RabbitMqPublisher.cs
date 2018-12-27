using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Text;

namespace BasicRabbitMQClient
{
    public class RabbitMqPublisher : IRabbitMqPublisher
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;

        private readonly PublicationAddress _publicationAddress;
        private readonly string _message;

        /// <summary>
        /// RabbitMQ da var olan bir kuyruğa mesaj yayınlama sınıfı. Örnekleme sonrası Publish() metodu çağırılması gerekir.
        /// </summary>
        /// <param name="connectionProvider">Bağlantı bilgilerini içeren ConnectionProvider türünden paremetre</param>
        /// <param name="message">Kuyruğa eklenilecek mesaj</param>
        /// <param name="publicationAddress"></param>     
        public RabbitMqPublisher(IRabbitMqConnectionProvider connectionProvider, PublicationAddress publicationAddress, string message)
        {
            _connectionProvider = connectionProvider;
            _publicationAddress = publicationAddress;
            _message = message;

        }

        /// <summary>
        /// Mesajı ilgili kuyruğa ekle
        /// </summary>
        public void Publish()
        {
            try
            {
                using (var connection = _connectionProvider.GetRabbitMqConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var basicProperties = channel.CreateBasicProperties();

                        basicProperties.Persistent = true;

                        channel.BasicPublish(_publicationAddress, null, Encoding.UTF8.GetBytes(_message));
                        
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
