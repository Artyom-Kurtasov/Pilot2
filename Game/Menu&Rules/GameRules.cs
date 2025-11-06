using Game.Interfaces;

namespace GameRules
{
    public class Rules
    {
        private readonly IGameUI _gameUI;

        public Rules(IGameUI gameUI)
        {
            _gameUI = gameUI;
        }
        public void DisplayRules()
        {
            _gameUI.PrintToUI($"\n{Game.Properties.Resources.RuleString1}" +
                $"\n{Game.Properties.Resources.RuleString2}" +
                $"\n{Game.Properties.Resources.RuleString3}" +
                $"\n\n{Game.Properties.Resources.PressAnyKey}");

            _gameUI.WaitForUser();
        }
    }
}

