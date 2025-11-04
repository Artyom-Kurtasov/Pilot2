using GameRules;
using Game.Interfaces;

namespace GameMenu
{
    public class Menu : IMenu
    {
        private readonly Rules _rules;
        private readonly IGameUI _gameUI;
        private readonly ILanguage _language;
        private readonly IGameEngine _gameEngine;

        public Menu(IGameUI gameUI, ILanguage language, IGameEngine gameEngine, Rules rules)
        {
            _rules = rules;
            _gameEngine = gameEngine;
            _language = language;
            _gameUI = gameUI;
        }
        public async Task DisplayMenuAsync()
        {
            while (true)
            {
                _gameUI.ClearUI();
                DisplayMenu();
                string? choice = _gameUI.ReadUserInput();

                if (await ProcessSelectionAsync(choice))
                    break;
            }
        }

        public void DisplayMenu()
        {
            _gameUI.PrintInLineToUI($"-------- {Game.Properties.Resources.Menu} --------" +
                $"\n1. {Game.Properties.Resources.Start}" +
                $"\n2. {Game.Properties.Resources.Rules}" +
                $"\n3. {Game.Properties.Resources.ChangeLang}" +
                $"\n4. {Game.Properties.Resources.Exit}" +
                $"\n{Game.Properties.Resources.YourChoice}: ");
        }

        public void DisplayError()
        {
            _gameUI.PrintToUI($"\n{Game.Properties.Resources.InvalidValue}" +
                $"\n{Game.Properties.Resources.PressAnyKey}");
        }

        public async Task<bool> ProcessSelectionAsync(string? choice)
        {
            switch (choice)
            {
                case "1":
                    await _gameEngine.StartGameAsync();
                    return false;

                case "2":
                    _rules.DisplayRules();
                    return false;

                case "3":
                    _language.ChooseLanguage();
                    return false;

                case "4":
                    return true;

                default:
                    DisplayError();
                    _gameUI.WaitForUser();
                    return false;
            }
        }
    }
}


