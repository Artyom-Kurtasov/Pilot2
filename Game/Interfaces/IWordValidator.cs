using Game.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Interfaces
{
    public interface IWordValidator
    {
        bool HasMoreLettersThanStartWord(Dictionary<char, int> charOfStartWord, int countOfLetter, char letterOfWord);
        bool ContainsLetter(Dictionary<char, int> charOfStartWord, char letterOfWord);
        bool IsWordAlreadyUsed(List<string> usedWords, string input);
        bool IsLengthValid(string word);
        bool ContainsInvalidCharacters(string input);
    }
}
