using System;
using System.Collections.Generic;
using System.Text;

namespace BasicRabbitMQClient
{
    public interface IRabbitMqPublisher
    {
        void Publish();
    }
}
