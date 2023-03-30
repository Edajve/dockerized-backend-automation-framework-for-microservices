namespace producer.TestUtils
{
    public static class TestConstants
    {
        public static class Kafka
        {
            public const string BootstrapServers = "localhost:29092";
            
            public const string OpinionApplicatorTopicName = "pa.cbb.oa";
            public const string OpinionApplicatorRulesChangedTopicName = "pa.cbb.oa.rules";
            public const string PricingPlatformTopicName = "pa.cbb.oa.autotraderproducer";
            public const string BlenderTopicName = "pa.cbb.blender";
        }

        public static class Mysql
        {
            public const string Server = "localhost";
            public const int Port = 33060;
            public const string Username = "root";
            public const string Password = "Password12!";
            public const string DatabaseName = "pa-cbb-oa";
            public const string RuleDimensionTableName = "RuleDimension";
            public const string RuleTableName = "Rule";

        }
        
        public static class OpinionApplicator
        {
            public const string Url = "http://localhost:5000";
        }
    }
}