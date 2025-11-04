global using System;
global using System.Collections.Generic;
global using System.Globalization;
global using System.Linq;
global using System.Text.RegularExpressions;
global using System.Threading.Tasks;
using Game.GameData;
using Game.GameLanguage;
using Game.GameUI;
using Game.Interfaces;
using Game.Services;
using GameMenu;
using GameRules;

internal class Program
{
    public async static Task Main(string[] args)
    {
        IGameUI gameUI = new UI();
        GameState gameState = new GameState();
        IGameTimer timer = new GameTimer();
        IWordValidator wordValidator = new WordValidator(); 
        CommandValidator commandValidator = new CommandValidator();

        CommandLogic commandLogic = new CommandLogic(gameState, gameUI);
        IGameLogic gameLogic = new GameLogic(gameState, commandLogic, commandValidator, wordValidator);
        IGameTextLogic textLogic = new GameTextLogic(gameState, gameUI);
        LanguageOptions languageOptions = new LanguageOptions();
        ILanguage languageService = new GameLanguage(gameUI, languageOptions);
        IGameEngine gameEngine = new GameEngine((GameLogic)gameLogic, gameUI, wordValidator, textLogic, timer, gameState);
        Rules rules = new Rules(gameUI);
        IMenu menu = new Menu(gameUI, languageService, gameEngine, rules);

        languageService.ChooseLanguage();
        await menu.DisplayMenuAsync();
    }
}

