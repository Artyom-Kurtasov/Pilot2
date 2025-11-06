using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Interfaces
{
    public interface IGameLogic
    {
        bool ValidateCommands(string input);
        string? ValidateStartWord(string startWord);
        string? ValidateInputWord(string input);
        void SwitchPlayer();
        void AddWordToUsedWords(string input);
        void ClearUsedWords();
        void UpdatePlayerState();
        void DetermineWinner();
        void BuildLetterDictionary();
    }
}
