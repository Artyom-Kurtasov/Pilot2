using Game.GameData;
using Game.Interfaces;

namespace Game.GameLanguage
{
    public class Language : ILanguage
    {
        private readonly IGameUI _gameUI;
        private readonly LanguageOptions _langOptions;

        public Language(IGameUI gameUI, LanguageOptions langOptions)
        {
            _langOptions = langOptions;
            _gameUI = gameUI;
        }
        private void SetLanguage(string? choice)
        {
            _langOptions.CurrentLanguage = choice == "1" ? "ru" : "en";
        }

        private bool IsValidChoice(string? choice)
        {
            return choice == "1" || choice == "2";
        }

        private void DisplayMenu()
        {
            _gameUI.ClearUI();
            _gameUI.PrintInLineToUI("Выберите язык игры / Choose the game language:" +
                "\n1. Русский (Russian)" +
                "\n2. English (Английский)" +
                "\nВаш выбор (1 или 2) / Your choice (1 or 2): ");
        }

        private void DisplayError()
        {
            _gameUI.PrintToUI("\nНекорректно значение! / Invalid value!" +
                "\nНажмите любую клавишу для продолжения / Press any key to continue.");
        }

        private void DisplaySelectionMessage()
        {
            if (_langOptions.CurrentLanguage == "ru")
            {
                _gameUI.PrintToUI("\nВыбран русский язык." +
                    "\nНажмите любую клавишу для продолжения.");
            }
            else
            {
                _gameUI.PrintToUI("\nEnglish language chosen." +
                    "\nPress any key to continue.");
            }

        }


        private void ConfigureCulture()
        {
            var culture = new CultureInfo(_langOptions.CurrentLanguage ?? "ru");
            Game.Properties.Resources.Culture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        public void ChooseLanguage()
        {
            while (true)
            {
                DisplayMenu();
                string? choice = _gameUI.ReadUserInput();

                if (IsValidChoice(choice))
                {
                    SetLanguage(choice);
                    DisplaySelectionMessage();
                    ConfigureCulture();
                    _gameUI.WaitForUser();
                    _gameUI.ClearUI();
                    break;
                }

                DisplayError();
                _gameUI.WaitForUser();
            }
        }
    }

}
