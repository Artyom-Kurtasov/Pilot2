using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.GameData
{
    public class GameState
    {
        public string Player1Nickname {  get; set; }
        public string Player2Nickname {  get; set; }
        public string? Input { get; set; }
        public Dictionary<char, int> CharOfStartWord = new();
        public string? StartWord { get; set; }
        public string? Winner { get; set; }
        public  List<string> UsedWords = new();
        public  bool CurrentPlayer { get; set; } = false;
        public string? NameOfCurrentPlayer { get; set; }
    }
}
