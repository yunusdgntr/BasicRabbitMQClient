# BasicRabbitMQ
BasicRabbitMQ .netCore Example
#### This library has been created for simple uses only. See for advanced uses https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html

```cs
> Install-Package BasicRabbitMQClient -Version 1.0.2
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


### Publisher Example 
```cs
using System;
using BasicRabbitMQClient;
using RabbitMQ.Client;

namespace ConsoleApp2
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
                YourPublishMethod("Test Text", "testQuee");
                Console.WriteLine("Message added to queue...");
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occured:" + e);
                throw;
            }
            Console.ReadLine();
        }

        public static bool YourPublishMethod(string sendTextToQueue, string queueName)
        {
            //Parameters (hostname username password)
            var rabbitMqConnectionProvider = new RabbitMqConnectionProvider("192.168.1.2", "username", "password");
            var puplicationAdress = new PublicationAddress(ExchangeType.Direct, string.Empty, queueName);
            var rabbitMqPublisher = new RabbitMqPublisher(rabbitMqConnectionProvider, puplicationAdress, sendTextToQueue);
            rabbitMqPublisher.Publish();
            return true;
        }
    }
}
```
