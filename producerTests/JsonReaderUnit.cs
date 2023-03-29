using System.Reflection;


namespace producerTests;

public class JsonReaderUnit
{
    public class MarketOrchestratorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MarketOrchestratorTests_ReadGameDataFromFile_FileDoesntExist_ReturnsNull()
        {
            var gameDataReader = new GameDataReader.GameDataReader();

            var gameData = gameDataReader.ReadFromJson("doesntExist.json");

            Assert.IsNull(gameData);
        }

        [Test]
        public void MarketOrchestratorTests_ReadTestGameDataFromFile_Success()
        {
            var gameDataReader = new GameDataReader.GameDataReader();

            // var gameData =
            //     gameDataReader.ReadFromJson(
            //         $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}/producerTests/test.txt");
            
            var gameData =
                gameDataReader.ReadFromJson("/Users/dajve.echols/personalProjects/cSharp/kafka_basics/producerTests/TestData/test.txt");

            Assert.That(gameData, Is.EqualTo("Hello World"));
        }

        [Test]
        public void MarketOrchestratorTests_ReadRealGameData_Success()
        {
            var gameDataReader = new GameDataReader.GameDataReader();

            var gameData =
                gameDataReader.ReadFromJson("/Users/dajve.echols/personalProjects/cSharp/kafka_basics/producer/GameState.json");

            var expectedJson =
                "{\"MessageType\":5,\"Message\":{\"Data\":{\"Fields\":{\"StartTime\":" +
                "{\"NullValue\":0,\"NumberValue\":0,\"StringValue\":\"2023-01-29T23:30:00\"," +
                "\"BoolValue\":false,\"StructValue\":null,\"ListValue\":null,\"KindCase\":3}," +
                "\"FixtureName\":{\"NullValue\":0,\"NumberValue\":0,\"StringValue\":" +
                "\"" + "Cincinnati Bengals At Kansas City Chiefs\",\"BoolValue\":false,\"StructValue\":null," +
                "\"ListValue\":null,\"KindCase\":3},\"Sport\":{\"NullValue\":0,\"NumberValue\":0," +
                "\"StringValue\":\"NFL\",\"BoolValue\":false,\"StructValue\":null,\"ListValue\":null," +
                "\"KindCase\":3},\"HomeTeam\":{\"NullValue\":0,\"NumberValue\":0,\"StringValue\":\"Kansas City Chiefs\"," +
                "\"BoolValue\":false,\"StructValue\":null,\"ListValue\":null,\"KindCase\":3},\"AwayTeam\":{\"NullValue\":0," +
                "\"NumberValue\":0,\"StringValue\":\"Cincinnati Bengals\",\"BoolValue\":false,\"StructValue\":null," +
                "\"ListValue\":null,\"KindCase\":3}}}},\"RampId\":\"10219666\"}";

            Assert.That(gameData, Is.EqualTo(expectedJson));
        }
    }
}