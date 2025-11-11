using GameRules;
using Game.Interfaces;
using Game.PlayerManagement;

namespace GameMenu
{
    public class Menu : IMenu
    {
        private readonly PlayerManager _playerManager;
        private IJsonServices _JsonServices;
        private readonly Rules _rules;
        private readonly IGameUI _gameUI;
        private readonly ILanguage _language;
        private readonly IGameEngine _gameEngine;

        public Menu(IGameUI gameUI, ILanguage language, IGameEngine gameEngine, Rules rules, IJsonServices jsonServices, PlayerManager playerManager)
        {
            _rules = rules;
            _gameEngine = gameEngine;
            _language = language;
            _gameUI = gameUI;
            _JsonServices = jsonServices;
            _playerManager = playerManager;
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
                $"\n4. {Game.Properties.Resources.ChangeNicknames}" +
                $"\n5. {Game.Properties.Resources.Exit}" +
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
                    _JsonServices.FillFile();
                    return false;

                case "2":
                    _rules.DisplayRules();
                    return false;

                case "3":
                    _language.ChooseLanguage();
                    return false;

                case "4":
                    _playerManager.SetPlayersNicknames();
                    return false;
                case "5":
                    return true;

                default:
                    DisplayError();
                    _gameUI.WaitForUser();
                    return false;
            }
        }
    }
}


