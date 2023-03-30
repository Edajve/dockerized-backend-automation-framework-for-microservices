using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using producer.TestUtils;

namespace producer.Contexts;

public class KafkaContext
{
    
        public async Task InitAsync()
        {
            await WaitForKafka();

            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = TestConstants.Kafka.BootstrapServers }).Build();

            try
            {
                var topics = new[] { TestConstants.Kafka.OpinionApplicatorTopicName, TestConstants.Kafka.OpinionApplicatorRulesChangedTopicName, TestConstants.Kafka.PricingPlatformTopicName,  TestConstants.Kafka.BlenderTopicName };

                var deleted = await TryDeleteTopics(topics, adminClient);

                if (deleted)
                {
                    var created = await TryCreateTopics(topics, adminClient);
                    if (!created)
                    {
                        throw new Exception("Topics were not created");
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        public async Task TearDownAsync()
        {
            using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = TestConstants.Kafka.BootstrapServers }).Build();

            try
            {
                await TryDeleteTopics(new[] { TestConstants.Kafka.OpinionApplicatorTopicName, TestConstants.Kafka.OpinionApplicatorRulesChangedTopicName, TestConstants.Kafka.PricingPlatformTopicName,  TestConstants.Kafka.BlenderTopicName }, adminClient);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        private async Task<bool> TryCreateTopics(string[] topics, IAdminClient adminClient)
        {
            var metadata = adminClient.GetMetadata(TimeSpan.FromMilliseconds(1000));

            var topicsToCreate = new List<TopicSpecification>();
            foreach (var topic in topics)
            {
                if (metadata.Topics.All(x => !string.Equals(x.Topic, topic, StringComparison.OrdinalIgnoreCase)))
                {
                    topicsToCreate.Add(new TopicSpecification
                    {
                        Name = topic,
                        NumPartitions = 1,
                        ReplicationFactor = 1
                    });
                }
            }

            await adminClient.CreateTopicsAsync(topicsToCreate);

            var allTopicsCreated = false;
            await TestHelper.WaitForConditionAsync(() =>
            {
                metadata = adminClient.GetMetadata(TimeSpan.FromMilliseconds(1000));

                allTopicsCreated = topicsToCreate.All(topicToCreate => metadata.Topics.Any(topicMetadata => string.Equals(topicMetadata.Topic, topicToCreate.Name, StringComparison.OrdinalIgnoreCase)));

                return allTopicsCreated;
            });

            return allTopicsCreated;
        }

        private async Task<bool> TryDeleteTopics(string[] topics, IAdminClient adminClient)
        {
            var metadata = adminClient.GetMetadata(TimeSpan.FromMilliseconds(1000));

            var topicsToDelete = new List<string>();
            foreach (var topic in topics)
            {
                if (metadata.Topics.Any(x => string.Equals(x.Topic, topic)))
                {
                    topicsToDelete.Add(topic);
                }
            }

            if (topicsToDelete.Any())
            {
                await adminClient.DeleteTopicsAsync(topicsToDelete.ToArray());
            }

            var allTopicDeleted = false;
            await TestHelper.WaitForConditionAsync(() =>
            {
                metadata = adminClient.GetMetadata(TimeSpan.FromMilliseconds(1000));

                allTopicDeleted = topicsToDelete.All(topicToDelete => metadata.Topics.All(topicMetadata => !string.Equals(topicMetadata.Topic, topicToDelete, StringComparison.OrdinalIgnoreCase)));

                return allTopicDeleted;
            });

            return allTopicDeleted;
        }

        private async Task WaitForKafka()
        {
            await TestHelper.WaitForConditionAsync(() =>
            {
                try
                {
                    using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = TestConstants.Kafka.BootstrapServers }).Build();
                    var metadata = adminClient.GetMetadata(TimeSpan.FromMilliseconds(1000));

                    return metadata.Topics.Any(x => x.Topic == TestConstants.Kafka.OpinionApplicatorTopicName);
                }
                catch
                {
                    return false;
                }
            });
        }
}