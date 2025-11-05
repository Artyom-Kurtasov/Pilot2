using Game.GameData;
using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Services
{
    public class CommandLogic
    {
        private readonly GameState _state;
        private readonly IGameUI _gameUI;

        public CommandLogic(GameState state, IGameUI gameUI)
        {
            _state = state;
            _gameUI = gameUI;
        }
        public void ShowAllWordsOfThisGame() // will be changed
        {
            if (_state.UsedWords.Count == 0) return;

            _gameUI.PrintToUI($"\nПервоначальное слово: {_state.StartWord}");
            _gameUI.PrintToUI("Слова первого игрока:                        Слова второго игрока:");

            for (int i = 1; i < _state.UsedWords.Count; i++)
            {
                if (i % 2 == 0)
                    _gameUI.PrintToUI($"                                             {_state.UsedWords[i]}");
                else
                    _gameUI.PrintToUI($"{_state.UsedWords[i]}");
            }
        }
        public void ShowErrorMessage()
        {
            _gameUI.PrintToUI("Incorrect command!"); // add to resources files!!!
        }
    }
}
