namespace producer.TestUtils
{
    public static class TestConstants
    {
        public static class Kafka
        {
            public const string BootstrapServers = "localhost:29092";
            
            public const string KafkaNflLiveTopic = "KAFKA_NFL_LIVE_TOPIC";
            public const string KafkaNbaLiveTopic = "KAFKA_NBA_LIVE_TOPIC";
            public const string GenuisLiveEvents= "genius_live_events";
            public const string PricingModelData = "pricing_model_data";
            public const string PricingPlayerGameData = "pricing_player_game_data";
            public const string KafkaClientId = "rt-market-orchestrator";
            public const string KafkaLiveEventsTopic = "live-events";
            public const string KafkaPricingTopic = "pricing-data";
            public const string KafkaGamePricingServers = "pricing_player_game_data";
        }

        
        
        public static class MarketOrchestratorApplication
        {
            //public const string Url = "http://localhost:5000";
            public const string Url = "http://localhost:5171";
        }
    }
}