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
        public void ShowAllWordsOfThisGame()
        {
            if (_state.UsedWords.Count == 0) return;

            _gameUI.PrintToUI($"\nПервоначальное слово: {_state.StartWord}\n");
            _gameUI.PrintToUI("Слова первого игрока".PadRight(26) + "Слова второго игрока");
            _gameUI.PrintToUI(new string('─', 50));

            for (int i = 1; i < _state.UsedWords.Count; i += 2)
            {
                string player1Word = _state.UsedWords[i].PadRight(25);
                string player2Word = (i + 1 < _state.UsedWords.Count) ? _state.UsedWords[i + 1] : "-";

                _gameUI.PrintToUI($"{player1Word} {player2Word}");
            }
        }
        public void ShowErrorMessage()
        {
            _gameUI.PrintToUI("Incorrect command!"); // add to resources files!!!
        }
    }
}
