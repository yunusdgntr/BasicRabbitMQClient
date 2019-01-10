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
        /// The Publish () method should be called after sampling.
        /// </summary>
        /// <param name="connectionProvider">ConnectionProvider have connections info</param>
        /// <param name="message">Message you want to add to the queue </param>
        /// <param name="publicationAddress"></param>     
        public RabbitMqPublisher(IRabbitMqConnectionProvider connectionProvider, PublicationAddress publicationAddress, string message)
        {
            _connectionProvider = connectionProvider;
            _publicationAddress = publicationAddress;
            _message = message;

        }

        /// <summary>
        /// Add message to Queue 
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
