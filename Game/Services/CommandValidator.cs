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
        public bool CommandForWords(string input) => input == "/show-words";
    }
}
