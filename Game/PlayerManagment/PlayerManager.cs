using Game.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.PlayerManagment
{
    public class PlayerManager
    {
        private readonly GameState _gameState;
        private readonly PlayerNicknameService _nicknameService;

        public PlayerManager(GameState gameState, PlayerNicknameService nicknameService)
        {
            _gameState = gameState;
            _nicknameService = nicknameService;
        }

        public void SetPlayersNicknames()
        {
            _gameState.Player1Nickname = _nicknameService.GetValidNickname($"{Game.Properties.Resources.Player1Input}");
            _gameState.Player2Nickname = _nicknameService.GetValidNickname($"{Game.Properties.Resources.Player2Input}");
        }
    }
}
