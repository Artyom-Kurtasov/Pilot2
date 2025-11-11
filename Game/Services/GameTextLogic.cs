using Game.GameData;
using Game.Interfaces;


namespace Game.Services
{
    public class GameTextLogic : IGameTextLogic
    {
        private readonly GameState _state;
        private readonly IGameUI _gameUI;
        public GameTextLogic(GameState state, IGameUI gameUI)
        {
            _state = state;
            _gameUI = gameUI;
        }
        public void DisplayEndGameMessage()
        {
            string timesUp = Game.Properties.Resources.TimesUp.Replace("{nameOfCurrentPlayer}", $"{_state.NameOfCurrentPlayer}");
            string winner = Game.Properties.Resources.Winner.Replace("{nameOfCurrentPlayer}", $"{_state.Winner}");
            string pressAnyKey = Game.Properties.Resources.PressAnyKey;

            _gameUI.PrintToUI($"\n{timesUp} {winner}" +
                $"\n{pressAnyKey}");
        }

        public void DisplayRoundInfo()
        {
            string writeWord = Game.Properties.Resources.WriteWord.Replace("{nameOfCurrentPlayer}", $"{_state.NameOfCurrentPlayer}");
            string startWord = Game.Properties.Resources.StartWord;
            _gameUI.PrintToUI($"\n{startWord}{_state.StartWord}");
            _gameUI.PrintInLineToUI($"{writeWord}");
        }
    }
}