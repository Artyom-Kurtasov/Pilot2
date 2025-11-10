using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    public class GameConstants
    {
        public const string PATH = "DataBase.json";
        public const int MinCharactersInWord = 8;
        public const int MaxCharactersInWord = 30;
        public const string ValidSymbols = @"[^a-zA-Zа-яА-Я\\/]";
        public const int TurnTimeLimit = 20;
        public const int TimerIntervalMs = 1000;
    }
}
