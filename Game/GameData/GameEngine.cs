using Game.GameData;
using Game.Interfaces;
using Game.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    public class GameEngine : IGameEngine
    {
        private readonly IGameTimer _timer;
        private readonly IGameTextLogic _textLogic;
        private readonly IWordValidator _wordValidator;
        private readonly IGameLogic _gameLogic;
        private readonly IGameUI _gameUI;
        private readonly GameState _state;

        public GameEngine(GameLogic gameLogic, IGameUI gameUI, IWordValidator wordValidator, IGameTextLogic textLogic, IGameTimer timer, GameState state)
        {
            _timer = timer;
            _textLogic = textLogic;
            _wordValidator = wordValidator;
            _state = state;
            _gameUI = gameUI;
            _gameLogic = gameLogic;
        }

        public async Task StartGameAsync()
        {
            _gameUI.ClearUI();
            _gameLogic.ClearUsedWords();
            SetStartWord();
            _gameLogic.BuildLetterDictionary();
            StartGameLoop();
        }

        private void SetStartWord()
        {
            while (true)
            {
                _gameUI.PrintInLineToUI($"\n{Game.Properties.Resources.WriteF8T30}: ");
                string? input = _gameUI.ReadUserInput();
                _state.StartWord = input;

                if (_wordValidator.IsLengthValid(input ?? "")
                    && !_wordValidator.ContainsInvalidCharacters(input ?? ""))
                {
                    _gameLogic.AddWordToUsedWords(input ?? "");
                    break;
                }

                _gameUI.PrintToUI(_gameLogic.ValidateStartWord(input ?? "") ?? "");
                _gameUI.PrintToUI($"\n{Game.Properties.Resources.PressAnyKey}");
                _gameUI.WaitForUser();
                _gameUI.ClearUI();
            }
        }

        private void StartGameLoop()
        {
            while (true)
            {
                _gameLogic.UpdatePlayerState();
                _textLogic.DisplayRoundInfo();

                _timer.StartTimer();
                string? input = _gameUI.ReadUserInput();
                _state.Input = input;

                if (_gameLogic.ValidateCommands(input ?? ""))
                {
                    continue;
                }

                if (_timer.IsTimerUp)
                {
                    _gameLogic.DetermineWinner();
                    _textLogic.DisplayEndGameMessage();
                    _gameUI.WaitForUser();
                    return;
                }

                _gameUI.PrintToUI(_gameLogic.ValidateInputWord(input ?? "") ?? "");
            }
        }
    }
}