using Game.GameData;
using Game.Interfaces;
using System.Text.Json;

namespace Game.Services
{
    public class CommandLogic : ICommandLogic
    {
        private readonly string _path;
        private readonly GameState _gameState;
        private readonly IGameUI _gameUI;

        public CommandLogic(GameState state, IGameUI gameUI)
        {
            _gameState = state;
            _gameUI = gameUI;
            _path = GameConstants.PATH;
        }
        public void ShowAllWordsOfThisGame()
        {
            _gameUI.InformationColor();
            if (_gameState.UsedWords.Count - 1 == 0)
            {
                _gameUI.PrintToUI("");
                _gameUI.PrintToUI($"{Game.Properties.Resources.NoWords}");
            }
            else
            {
                _gameUI.PrintToUI($"\n{Game.Properties.Resources.StartWord}: {_gameState.StartWord}\n");
                _gameUI.PrintToUI($"{Game.Properties.Resources.FirstPlayerWords}".PadRight(26) + $"{Game.Properties.Resources.SecondPlayerWords}");
                _gameUI.PrintToUI(new string('─', 50));

                for (int i = 1; i < _gameState.UsedWords.Count; i += 2)
                {
                    string player1Word = _gameState.UsedWords[i].PadRight(25);
                    string player2Word = (i + 1 < _gameState.UsedWords.Count) ? _gameState.UsedWords[i + 1] : "-";

                    _gameUI.PrintToUI($"{player1Word} {player2Word}");
                }
            }

            _gameUI.StandartColor();
        }

        public void DisplayTotalScore()
        {
            _gameUI.InformationColor();
            _gameUI.PrintToUI("");
            _gameUI.PrintToUI($"{Game.Properties.Resources.TotalScore}");
            _gameUI.PrintToUI(new string('-', 50));

            var scores = GetScoreData();
            foreach (var kvp in scores)
            {
                _gameUI.PrintToUI($"{kvp.Key}: {kvp.Value}");
            }

            _gameUI.StandartColor();
        }

        public void DisplayScore()
        {
            _gameUI.InformationColor();
            _gameUI.PrintToUI("");
            _gameUI.PrintToUI($"{Game.Properties.Resources.Score}");
            _gameUI.PrintToUI(new string('-', 50));

            var scores = GetCurrentPlayersScore();
            foreach (var kvp in scores)
            {
                _gameUI.PrintToUI($"{kvp.Key}: {kvp.Value}");
            }

            _gameUI.StandartColor();
        }

        public void ShowErrorMessage()
        {
            _gameUI.ErrorColor();
            _gameUI.PrintToUI($"\n{Game.Properties.Resources.IncorrectCommand}");
            _gameUI.StandartColor();
        }

        private Dictionary<string, int> GetScoreData()
        {
            var usersState = TryReadScoreFile();

            usersState.TryAdd(_gameState.Player1Nickname, _gameState.Player1Score);
            usersState.TryAdd(_gameState.Player2Nickname, _gameState.Player2Score);

            return usersState;
        }

        private Dictionary<string, int> TryReadScoreFile()
        {
            if (!File.Exists(_path))
                return new Dictionary<string, int>();

            string json = File.ReadAllText(_path);
            if (string.IsNullOrWhiteSpace(json))
                return new Dictionary<string, int>();

            var data = JsonSerializer.Deserialize<Dictionary<string, int>>(json);
            return data ?? new Dictionary<string, int>();
        }


        private Dictionary<string, int> GetCurrentPlayersScore()
        {
            return new Dictionary<string, int>
            {
                { _gameState.Player1Nickname, _gameState.UsersState.GetValueOrDefault(_gameState.Player1Nickname, 0) },
                { _gameState.Player2Nickname, _gameState.UsersState.GetValueOrDefault(_gameState.Player2Nickname, 0) }
            };
        }

    }
}
