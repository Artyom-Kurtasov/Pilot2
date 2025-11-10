using System.Text.Json;
using Game.PlayerManagement;
using Game.GameData;
using Game.Interfaces;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Game.FileServices
{
    public class JsonServices : IJsonServices
    {

        private readonly GameState _gameState;
        private readonly string _path;
        public JsonServices(GameState gameState)
        {
            _gameState = gameState;
            _path = GameConstants.PATH;
        }
        public void FillFile()
        {
            AddPlayersState();
            SaveToFile();
        }
        public void ReadFile()
        {
            if (!File.Exists(_path))
            {
                _gameState.UsersState = new Dictionary<string, int>();
                return;
            }

            string json = File.ReadAllText(_path);

            if (string.IsNullOrWhiteSpace(json))
            {
                _gameState.UsersState = new Dictionary<string, int>();
                return;
            }

            var data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            _gameState.UsersState = data ?? new Dictionary<string, int>();
        }
        private void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            byte[] jsonUtf8Bytes = JsonSerializer.SerializeToUtf8Bytes(_gameState.UsersState, options);
            File.WriteAllBytes(_path, jsonUtf8Bytes);
        }

        private void AddPlayersState()
        {
            if (!_gameState.UsersState.ContainsKey(_gameState.Player1Nickname))
            {
                _gameState.UsersState.Add(_gameState.Player1Nickname, _gameState.Player1Score);
            }
            else
            {
                _gameState.UsersState[_gameState.Player1Nickname] = _gameState.Player1Score;
            }

            if (!_gameState.UsersState.ContainsKey(_gameState.Player2Nickname))
            {
                _gameState.UsersState.Add(_gameState.Player2Nickname, _gameState.Player2Score);
            }
            else
            {
                _gameState.UsersState[_gameState.Player2Nickname] = _gameState.Player2Score;
            }
        }
    }
}
