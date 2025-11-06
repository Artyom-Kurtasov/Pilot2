using Game.GameData;
using Game.Interfaces;
using Game.PlayerManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Game.Services
{
    public class GameLogic : IGameLogic
    {
        private readonly GameState _gameState;
        private readonly IWordValidator _wordValidator;
        private readonly CommandLogic _commandLogic;
        private readonly CommandValidator _commandValidator;
        private readonly PlayerState _playerState;

        public GameLogic(GameState state, CommandLogic commandLogic, CommandValidator commandValidator, IWordValidator wordValidator, PlayerState playerState)
        {
            _playerState = playerState;
            _gameState = state;
            _wordValidator = wordValidator;
            _commandLogic = commandLogic;
            _commandValidator = commandValidator;
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
            if (_commandValidator.CommandForWords(input))
            {
                _commandLogic.ShowAllWordsOfThisGame();
                return true;
            }
            else if (_commandValidator.IncorrectCommand(input))
            {
                _commandLogic.ShowErrorMessage();
                return true;
            }
                return false;
        }
    }
}
