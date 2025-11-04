using Game.GameData;
using Game.Interfaces;
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
        private readonly GameState _state;
        private readonly IWordValidator _wordValidator;
        private readonly CommandLogic _commandLogic;
        private readonly CommandValidator _commandValidator;

        public GameLogic(GameState state, CommandLogic commandLogic, CommandValidator commandValidator, IWordValidator wordValidator)
        {
            _state = state;
            _wordValidator = wordValidator;
            _commandLogic = commandLogic;
            _commandValidator = commandValidator;
        }
        public void SwitchPlayer() => _state.CurrentPlayer = !_state.CurrentPlayer;

        public void AddWordToUsedWords(string input) => _state.UsedWords.Add(input ?? "");

        public void ClearUsedWords() => _state.UsedWords.Clear();

        public void UpdatePlayerState()
        {
            _state.NameOfCurrentPlayer = _state.CurrentPlayer
                ? Game.Properties.Resources.SecondPlayer
                : Game.Properties.Resources.FirstPlayer;
        }
        public void DetermineWinner()
        {
            _state.Winner = _state.CurrentPlayer
                ? Game.Properties.Resources.FirstPlayer
                : Game.Properties.Resources.SecondPlayer;
        }

        public void BuildLetterDictionary()
        {
            _state.CharOfStartWord.Clear();

            foreach (char letter in _state.StartWord ?? "")
            {
                if (_state.CharOfStartWord.ContainsKey(letter))
                    _state.CharOfStartWord[letter]++;
                else
                    _state.CharOfStartWord[letter] = 1;
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

            if (_wordValidator.IsWordAlreadyUsed(_state.UsedWords, input))
                return $"\n{Game.Properties.Resources.TryAnother}";

            foreach (char letterOfWord in input.Distinct())
            {
                int _countOfLetter = input.Count(letter => letter == letterOfWord);

                if (!_wordValidator.ContainsLetter(_state.CharOfStartWord, letterOfWord))
                    return $"\n{Game.Properties.Resources.NoLetter.Replace("{letterOfWord}", $"{letterOfWord}")}";
                else if (_wordValidator.HasMoreLettersThanStartWord(_state.CharOfStartWord, _countOfLetter, letterOfWord))
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
