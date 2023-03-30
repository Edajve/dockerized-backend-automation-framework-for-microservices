using System.Reflection;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Newtonsoft.Json;
using producer;


namespace producerTests;

public class JsonReaderUnit
{
    public class MarketOrchestratorTests
    {
        // private GameState _gameState = default!;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MarketOrchestratorTests_ReadGameDataFromFile_FileDoesntExist_ReturnsNull()
        {
            var gameDataReader = new GameDataReader();

            var gameData = gameDataReader.ReadFromJson("doesntExist.json");

            Assert.IsNull(gameData);
        }

        [Test]
        public void MarketOrchestratorTests_ReadTestGameDataFromFile_Success()
        {
            var gameDataReader = new GameDataReader();

            var gameData =
                gameDataReader.ReadFromJson(
                    "/Users/dajve.echols/personalProjects/cSharp/kafka_basics/producerTests/TestData/test.txt");

            Assert.That(gameData, Is.EqualTo("Hello World"));
        }

        [Test]
        public void MarketOrchestratorTests_ReadRealGameData_Success()
        {
            var gameDataReader = new GameDataReader();

            var gameData =
                gameDataReader.ReadFromJson(
                    "/Users/dajve.echols/personalProjects/cSharp/kafka_basics/producer/GameState.json");

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

        [Test]
        public void MarketOrchestratorTests_Create_JSON_From_Class_Object_Returns_JSON_With_Different_Values()
        {
            // var gameState = new GameState
            // {
            //     new GameState.RootObject
            //     {
            //         MessageType = 0,
            //         Message = new GameState.Message
            //         {
            //             Data = new GameState.Data
            //             {
            //                 Fields = new GameState.Fields
            //                 {
            //                     StartTime = new GameState.StartTime
            //                     {
            //                         NullValue = 0,
            //                         NumberValue = 0,
            //                         StringValue = "000",
            //                         BoolValue = false,
            //                         StructValue = null,
            //                         ListValue = null,
            //                         KindCase = 0
            //                     },
            //                     FixtureName = new GameState.FixtureName
            //                     {
            //                         NullValue = 0,
            //                         NumberValue = 0,
            //                         StringValue = "0",
            //                         BoolValue = false,
            //                         StructValue = null,
            //                         ListValue = null,
            //                         KindCase = 0
            //                     },
            //                     Sport = new GameState.Sport
            //                     {
            //                         NullValue = 0,
            //                         NumberValue = 0,
            //                         StringValue = "0",
            //                         BoolValue = false,
            //                         StructValue = null,
            //                         ListValue = null,
            //                         KindCase = 0
            //                     },
            //                     HomeTeam = new GameState.HomeTeam
            //                     {
            //                         NullValue = 0,
            //                         NumberValue = 0,
            //                         StringValue = "0",
            //                         BoolValue = false,
            //                         StructValue = null,
            //                         ListValue = null,
            //                         KindCase = 0
            //                     },
            //                     AwayTeam = new GameState.AwayTeam
            //                     {
            //                         NullValue = 0,
            //                         NumberValue = 0,
            //                         StringValue = "0",
            //                         BoolValue = false,
            //                         StructValue = null,
            //                         ListValue = null,
            //                         KindCase = 0
            //                     }
            //                 }
            //             }
            //         },
            //         RampId = 00000000
            //     }
            // };
            // string jsonString = JsonSerializer.Serialize(gameState);
        }
    }
}