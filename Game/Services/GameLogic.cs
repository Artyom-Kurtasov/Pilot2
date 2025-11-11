using Game.GameData;
using Game.Interfaces;

namespace Game.Services
{
    public class GameLogic : IGameLogic
    {
        private readonly GameState _gameState;
        private readonly IWordValidator _wordValidator;
        private readonly ICommandLogic _commandLogic;
        private readonly CommandValidator _commandValidator;
        private readonly IJsonServices _jsonServices;

        public GameLogic(GameState state, ICommandLogic commandLogic, CommandValidator commandValidator, IWordValidator wordValidator, IJsonServices jsonServices)
        {
            _gameState = state;
            _wordValidator = wordValidator;
            _commandLogic = commandLogic;
            _commandValidator = commandValidator;
            _jsonServices = jsonServices;

        }
        public void SwitchPlayer() => _gameState.CurrentPlayer = !_gameState.CurrentPlayer;

        public void AddWordToUsedWords(string input) => _gameState.UsedWords.Add(input ?? "");

        public void ClearUsedWords() => _gameState.UsedWords.Clear();

        public void UpdatePlayerState()
        {
            _gameState.NameOfCurrentPlayer = _gameState.CurrentPlayer
                ? _gameState.Player2Nickname
                : _gameState.Player1Nickname;
        }
        public void DetermineWinner()
        {
            _gameState.Winner = _gameState.CurrentPlayer
                ? _gameState.Player1Nickname
                : _gameState.Player2Nickname;
        }

        public void BuildLetterDictionary()
        {
            _gameState.CharOfStartWord.Clear();

            foreach (char letter in _gameState.StartWord ?? "")
            {
                if (_gameState.CharOfStartWord.ContainsKey(letter))
                    _gameState.CharOfStartWord[letter]++;
                else
                    _gameState.CharOfStartWord[letter] = 1;
            }
        }

        public string? ValidateStartWord(string startWord)
        {
            if (_wordValidator.ContainsInvalidCharacters(startWord))
                return Game.Properties.Resources.NonAlphabet;
            else
                return $"\n{Game.Properties.Resources.InvalidLength}";
        }

        public string? ValidateInputWord(string input)
        {
            if (_wordValidator.ContainsInvalidCharacters(input))
                return $"\n{Game.Properties.Resources.NonAlphabet}";

            if (string.IsNullOrWhiteSpace(input))
                return $"\n{Game.Properties.Resources.CantBeNull}";

            if (_wordValidator.IsWordAlreadyUsed(_gameState.UsedWords, input))
                return $"\n{Game.Properties.Resources.TryAnother}";

            foreach (char letterOfWord in input.Distinct())
            {
                int _countOfLetter = input.Count(letter => letter == letterOfWord);

                if (!_wordValidator.ContainsLetter(_gameState.CharOfStartWord, letterOfWord))
                    return $"\n{Game.Properties.Resources.NoLetter.Replace("{letterOfWord}", $"{letterOfWord}")}";
                else if (_wordValidator.HasMoreLettersThanStartWord(_gameState.CharOfStartWord, _countOfLetter, letterOfWord))
                    return $"\n{Game.Properties.Resources.MoreThen.Replace("{letterOfWord}", $"{letterOfWord}")}";
            }

            SwitchPlayer();
            AddWordToUsedWords(input); 
            return $"\n{Game.Properties.Resources.CorrectWord}";
        }

        public bool ValidateCommands(string input)
        {
            if (_commandValidator.IsAllWords(input))
            {
                _commandLogic.ShowAllWordsOfThisGame();
                return true;
            }
            else if (_commandValidator.IsTotalScore(input))
            {
                _commandLogic.DisplayTotalScore();
                return true;
            }
            else if (_commandValidator.IsScore(input))
            {
                _commandLogic.DisplayScore();
                return true;
            }
            else if (_commandValidator.UnknownCommand(input))
            {
                _commandLogic.ShowErrorMessage();
                return true;
            }
                return false;
        }

        public void SetPoints()
        {
            if (_gameState.Player1Nickname == _gameState.Winner) _gameState.Player1Score++;
            else _gameState.Player2Score++;
        }

        public void ExitMethod(object sender, EventArgs e)
        {
            DetermineWinner();

            if (_gameState.Winner == _gameState.Player1Nickname)
                _gameState.Player1Score += 1;
            else
                _gameState.Player2Score += 1;

            _jsonServices.FillFile();
            System.Threading.Thread.Sleep(100);
        }
    }
}
