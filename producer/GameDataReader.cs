using System.IO;

namespace GameDataReader
{
    public class GameDataReader
    {
        public GameDataReader()
        {
        }

        public string? ReadFromJson(string jsonPath)
        {
            if (!File.Exists(jsonPath))
            {
                return null;
            }

            var gameData = string.Empty;

            using (var reader = new StreamReader(jsonPath))
            {
                while (!reader.EndOfStream)
                {
                    gameData = reader.ReadLine();
                }
            }

            return gameData;
        }
    }
}