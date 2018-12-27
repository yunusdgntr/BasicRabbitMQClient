using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace BasicRabbitMQClient
{
    public class RabbitMqConsumer : IRabbitMqConsumer
    {
        private readonly IRabbitMqConnectionProvider _connectionProvider;

        public RabbitMqConsumer(IRabbitMqConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        /// <summary>
        /// Kuyruktan mesajın alınıp işlenmesi.
        /// </summary>
        /// <param name="methodName">Kuruktan gelecek olan değeri string parametre olarak alacak metodunuz.Metod geri dönüşünüz true false olmalı.</param>
        /// <param name="queueName">Dinlenecek kuyruk ismi.</param>
        public void Consume(Func<string, bool> methodName, string queueName)
        {
            byte[] body;

            var connection = _connectionProvider.GetRabbitMqConnection();

            var channel = connection.CreateModel();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, basicDeliveryEvent) =>
            {
                try
                {
                    body = basicDeliveryEvent.Body;
                    var message = Encoding.UTF8.GetString(body);
                    methodName(message);
                    BasicAck(channel, false, basicDeliveryEvent);

                }
                catch (Exception ex)
                {
                    throw;
                }
            };
            channel.BasicConsume(queueName, false, consumer);
            //_logger.LogInformation("bla bla");

        }

        public void BasicAck(IModel model, bool multiple, BasicDeliverEventArgs deliverEventArgs)
        {
            try
            {
                model.BasicAck(deliverEventArgs.DeliveryTag, multiple);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
