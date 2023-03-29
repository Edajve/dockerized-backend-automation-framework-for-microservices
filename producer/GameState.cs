namespace GameDataObject;

    public class GameState
    {
        public class RootObject
        {
            public int MessageType { get; set; }
            public Message Message { get; set; }
            public string RampId { get; set; }
        }

        public class Message
        {
            public Data Data { get; set; }
        }

        public class Data
        {
            public Fields Fields { get; set; }
        }

        public class Fields
        {
            public StartTime StartTime { get; set; }
            public FixtureName FixtureName { get; set; }
            public Sport Sport { get; set; }
            public HomeTeam HomeTeam { get; set; }
            public AwayTeam AwayTeam { get; set; }
        }

        public class StartTime
        {
            public int NullValue { get; set; }
            public int NumberValue { get; set; }
            public string StringValue { get; set; }
            public bool BoolValue { get; set; }
            public object StructValue { get; set; }
            public object ListValue { get; set; }
            public int KindCase { get; set; }
        }

        public class FixtureName
        {
            public int NullValue { get; set; }
            public int NumberValue { get; set; }
            public string StringValue { get; set; }
            public bool BoolValue { get; set; }
            public object StructValue { get; set; }
            public object ListValue { get; set; }
            public int KindCase { get; set; }
        }

        public class Sport
        {
            public int NullValue { get; set; }
            public int NumberValue { get; set; }
            public string StringValue { get; set; }
            public bool BoolValue { get; set; }
            public object StructValue { get; set; }
            public object ListValue { get; set; }
            public int KindCase { get; set; }
        }

        public class HomeTeam
        {
            public int NullValue { get; set; }
            public int NumberValue { get; set; }
            public string StringValue { get; set; }
            public bool BoolValue { get; set; }
            public object StructValue { get; set; }
            public object ListValue { get; set; }
            public int KindCase { get; set; }
        }

        public class AwayTeam
        {
            public int NullValue { get; set; }
            public int NumberValue { get; set; }
            public string StringValue { get; set; }
            public bool BoolValue { get; set; }
            public object StructValue { get; set; }
            public object ListValue { get; set; }
            public int KindCase { get; set; }
        }
    }
