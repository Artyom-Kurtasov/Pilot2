using Game.GameData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Game.Interfaces;



namespace Game.Services
{
    public class WordValidator : IWordValidator
    {
        private readonly int _minCharactersInWord;
        private readonly int _maxCharactersInWord;
        private readonly string _validSymbols;

        public WordValidator()
        {
            _minCharactersInWord = GameConstants.MinCharactersInWord;
            _maxCharactersInWord = GameConstants.MaxCharactersInWord;
            _validSymbols = GameConstants.ValidSymbols;
        }

        public bool HasMoreLettersThanStartWord(Dictionary<char, int> charOfStartWord, int countOfLetter, char letterOfWord) =>
            countOfLetter > charOfStartWord[letterOfWord];

        public bool ContainsLetter(Dictionary<char, int> charOfStartWord, char letterOfWord) =>
            charOfStartWord.ContainsKey(letterOfWord);

        public bool IsWordAlreadyUsed(List<string> usedWords, string input) =>
            usedWords.Contains(input ?? "");

        public bool ContainsInvalidCharacters(string input) =>
            Regex.IsMatch(input, _validSymbols);

        public bool IsLengthValid(string word) =>
            word.Length >= _minCharactersInWord && word.Length <= _maxCharactersInWord;

    } 
}