using Confluent.Kafka;
using System;
using Microsoft.Extensions.Configuration;

class Producer {
    static void Main(string[] args)
    {
        if (args.Length != 1) {
            Console.WriteLine("Please provide the configuration file path as a command line argument");
        }

        IConfiguration configuration = new ConfigurationBuilder()
            .AddIniFile(args[0])
            .Build();

        const string topic = "dummy_topic_for_learning";
         
         string[] users = { "service 1", "service 2", "service 3", "service 4", "service 5", "service 6" };
         string[] items = { "message 1", "message 2", "message 3", "message 4", "message 5", "message 6" };

        using (var producer = new ProducerBuilder<string, string>(
                   configuration.AsEnumerable()).Build())
        {
            var numProduced = 0;
            Random rnd = new Random();
            const int numMessages = 10;
            for (int i = 0; i < numMessages; ++i)
            {
                var user = users[rnd.Next(users.Length)];
                var item = items[rnd.Next(items.Length)];
                
                //sending the message/producing message
                producer.Produce(topic, new Message<string, string> { Key = user, Value = item },
                    (deliveryReport) =>
                    {
                        if (deliveryReport.Error.Code != ErrorCode.NoError) {
                            Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                        }
                        else {
                            Console.WriteLine($"Produced event to topic {topic}: key = {user,-10} value = {item}");
                            numProduced += 1;
                        }
                    });
            }

            producer.Flush(TimeSpan.FromSeconds(10));
            Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
        }
    }
}