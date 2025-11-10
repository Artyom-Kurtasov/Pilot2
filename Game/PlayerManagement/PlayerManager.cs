using Game.FileServices;
using Game.GameData;
using System;
using Game.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.PlayerManagement
{
    public class PlayerManager
    {
        private readonly IGameUI _gameUI;
        private readonly GameState _gameState;
        private readonly PlayerNicknameService _nicknameService;

        public PlayerManager(GameState gameState, PlayerNicknameService nicknameService, IGameUI gameUI)
        {
            _gameState = gameState;
            _nicknameService = nicknameService;
            _gameUI = gameUI;
        }
        public void SetPlayersNicknames()
        {
            _gameUI.ClearUI();
            _gameState.Player1Nickname = null;
            _gameState.Player2Nickname = null;

            _gameState.Player1Nickname = _nicknameService.GetValidNickname($"{Game.Properties.Resources.Player1Input}");
            _gameState.Player2Nickname = _nicknameService.GetValidNickname($"{Game.Properties.Resources.Player2Input}");
            ResetPlayersScore();
        }
        private void ResetPlayersScore()
        {
            if (!_gameState.UsersState.ContainsKey(_gameState.Player1Nickname))
            {
                _gameState.Player1Score = 0;
            }
            else
            {
                _gameState.Player1Score = _gameState.UsersState[_gameState.Player1Nickname];
            }

            if (!_gameState.UsersState.ContainsKey(_gameState.Player2Nickname))
            {
                _gameState.Player2Score = 0;
            }
            else
            {
                _gameState.Player2Score = _gameState.UsersState[_gameState.Player2Nickname];
            }
        }

    }
}   
