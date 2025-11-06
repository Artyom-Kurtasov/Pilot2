using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Game.PlayerManagment;
using Game.GameData;

namespace Game.FileServices
{
    public class JsonServices  // will be changed!!!!
    {
        private readonly string PATH = "DataBase.json";
        private readonly GameState _gameState;

        public JsonServices(GameState gameState)
        {
            _gameState = gameState;
        }
        public void Write()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var data = new Dictionary<string, int>
            {
                [_gameState.Player1Nickname] = 5,
                [_gameState.Player2Nickname] = 10
            };

            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(data, options);
            File.WriteAllBytes(PATH, jsonUtf8Bytes);
        }

    }
}
