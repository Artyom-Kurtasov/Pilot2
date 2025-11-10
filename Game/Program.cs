global using System;
global using System.Collections.Generic;
global using System.Globalization;
global using System.Linq;
global using System.Text.RegularExpressions;
global using System.Threading.Tasks;
using Game.FileServices;
using Game.GameData;
using Game.GameLanguage;
using Game.GameUI;
using Game.Interfaces;
using Game.PlayerManagement;
using Game.Services;
using GameMenu;
using GameRules;
using Microsoft.Extensions.DependencyInjection;


internal class Program
{
    public async static Task Main(string[] args)
    {

        ServiceCollection services = new();
        var config = new ConfigureServices();
        config.Configure(services);

        using var servicesProvider = services.BuildServiceProvider();

        var languageService = servicesProvider.GetRequiredService<ILanguage>();
        var jsonServices = servicesProvider.GetRequiredService<IJsonServices>();
        var playerManager = servicesProvider.GetRequiredService<PlayerManager>();
        var menu = servicesProvider.GetRequiredService<IMenu>();

        languageService.ChooseLanguage();
        jsonServices.ReadFile();
        playerManager.SetPlayersNicknames();
        await menu.DisplayMenuAsync();
    }

}

