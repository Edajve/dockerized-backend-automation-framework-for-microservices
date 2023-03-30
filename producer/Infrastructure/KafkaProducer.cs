using System;
using System.Text;
using Confluent.Kafka;
using Newtonsoft.Json;
using producer.TestUtils;

namespace producer.Infrastructure;

public class KafkaProducer
{
    private readonly IProducer<byte[], byte[]> _producer;

    public KafkaProducer()
    {
        var config = new ProducerConfig
        {
            BootstrapServers = TestConstants.Kafka.BootstrapServers
        };

        _producer = new ProducerBuilder<byte[], byte[]>(config).Build();
    }

    public void Produce<TKey, TValue>(TKey key, TValue message)
    {
        var headers = new Headers
        {
            {"MessageType", Encoding.UTF8.GetBytes(typeof(TValue).Name)},
            {"CorrelationId", Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())}
        };

        var payload = new Message<byte[], byte[]>
        {
            Key = Encoding.ASCII.GetBytes(key.ToString()),
            Value = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message)),
            Headers = headers
        };

        _producer.Produce(TestConstants.Kafka.KafkaPricingTopic, payload, report =>
        {
            Console.WriteLine(report.Error.Code != ErrorCode.NoError
                ? $"KafkaProducer - Produce error - Code: {report.Error.Code}, Reason: {report.Error.Reason}"
                : "KafkaProducer - Produce success");
        });
    }
}