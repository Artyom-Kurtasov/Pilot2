using Game.FileServices;
using Game.GameData;
using Game.GameUI;
using Game.Interfaces;
using Game.PlayerManagement;
using GameMenu;
using GameRules;
using Game.GameLanguage;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Services
{
    public class ConfigureServices
    {
        public void Configure(IServiceCollection services)
        {
            services.AddSingleton<IGameUI, UI>();
            services.AddScoped<IMenu, Menu>();
            services.AddScoped<ILanguage, Language>();
            services.AddScoped<LanguageOptions>();

            services.AddScoped<IGameEngine, GameEngine>();
            services.AddScoped<IGameLogic, GameLogic>();
            services.AddScoped<IGameTextLogic, GameTextLogic>();
            services.AddScoped<IGameTimer, GameTimer>();
            services.AddScoped<IWordValidator, WordValidator>();
            services.AddScoped<CommandValidator>();
            services.AddScoped<ICommandLogic, CommandLogic>();
            services.AddScoped<Rules>();

            services.AddScoped<GameState>();
            services.AddScoped<IJsonServices, JsonServices>();
            services.AddScoped<PlayerManager>();
            services.AddScoped<PlayerNicknameService>();
        }
    }
}
