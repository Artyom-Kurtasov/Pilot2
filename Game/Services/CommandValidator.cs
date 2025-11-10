using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Services
{
    public class CommandValidator
    {
        public bool IncorrectCommand(string input)
        {
            return !string.IsNullOrEmpty(input) && input[0] == '/';
        }
        public bool IsAllWords(string input) => input == "/show-words";
        public bool IsTotalScore(string input) => input == "/total-score";
        public bool IsScore(string input) => input == "/score";
    }
}
