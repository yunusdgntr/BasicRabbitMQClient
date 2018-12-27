using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BasicRabbitMQClient
{
    public interface IRabbitMqConsumer
    {
        void Consume(Func<string, bool> myMethodNamestring, string queueName);

        void BasicAck(IModel model, bool multiple, BasicDeliverEventArgs deliverEventArgs);
    }
}
