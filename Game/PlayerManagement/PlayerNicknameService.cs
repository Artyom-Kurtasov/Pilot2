using Game.GameData;
using Game.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.PlayerManagement
{
    public class PlayerNicknameService
    {
        private readonly IGameUI _gameUI;
        private readonly GameState _gameState;

        public PlayerNicknameService(IGameUI gameUI, GameState gameState)
        {
            _gameUI = gameUI;
            _gameState = gameState;
        }

        public string GetValidNickname(string prompt)
        {
            while (true)
            {
                _gameUI.PrintInLineToUI(prompt);
                string nickname = _gameUI.ReadUserInput() ?? "";

                if (!string.IsNullOrWhiteSpace(nickname) && nickname != _gameState.Player1Nickname)
                {
                    _gameUI.ClearUI();
                    return nickname;
                }

                DisplayValidationError();
            }
        }

        private void DisplayValidationError()
        {
            _gameUI.PrintToUI($"{Game.Properties.Resources.NameCantBeNull}");
            _gameUI.PrintInLineToUI($"{Game.Properties.Resources.PressAnyKey}");
            _gameUI.WaitForUser();
            _gameUI.ClearUI();
        }
      
    }
}
