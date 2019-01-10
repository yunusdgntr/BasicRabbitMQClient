# BasicRabbitMQ
BasicRabbitMQ

```cs
> Install-Package BasicRabbitMQClient -Version 1.0.1
```

### Consumer Example 
```cs
using BasicRabbitMQClient;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        { 
            //Parameters (hostname username password)
            var rabbitMqConnectionProvider = new RabbitMqConnectionProvider("192.168.1.2", "username", "password");
            var rabbitMqConsumer = new RabbitMqConsumer(rabbitMqConnectionProvider);
            rabbitMqConsumer.Consume(YourMethod, "YourQueueName");
        }

        public static bool YourMethod(string incomingTextFromQueue)
        {
            //Your bussiness logic here.
            //Use the text came from Queue
            Console.WriteLine(incomingTextFromQueue+ " came from Queue");
            return true;
        }
    }
}
```