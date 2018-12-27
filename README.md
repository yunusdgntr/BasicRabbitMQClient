# BasicRabbitMQ
BasicRabbitMQ


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
            var rabbitMqConnectionProvider = new RabbitMqConnectionProvider("192.168.1.1", "test", "test");
            var rabbitMqConsumer = new RabbitMqConsumer(rabbitMqConnectionProvider);
            rabbitMqConsumer.Consume(YourMethod, "testqueue");
        }

        public static bool YourMethod(string text)
        {
            //Your bussiness logic here.
            //Use the text came from Queue
            Console.WriteLine(text+ " came from Queue");
            return true;
        }
    }
}
```