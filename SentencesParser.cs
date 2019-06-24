using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static char[] SeparationSigns = { '.', '!', '?', ';', ':', '(', ')' };

        public static void AddAndClearIfWordIsntEmpty(List<string> givenList, StringBuilder word)
        {
            if (word.Length > 0)
            {
                givenList.Add(word.ToString());
                word.Clear();
            }
        }

        public static List<string> ListWithAllWordsInString(string givenString)
        {
            var currentList = new List<string>();
            StringBuilder word = new StringBuilder();
            foreach (char symbol in givenString)
            {
                if (char.IsLetter(symbol) || symbol == '\'')
                    word.Append(char.ToLower(symbol));
                else AddAndClearIfWordIsntEmpty(currentList, word);
            }
            AddAndClearIfWordIsntEmpty(currentList, word);
            return currentList;
        }

        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            var tempSentencesArray = text.Split(SeparationSigns);
            foreach (string currentSentence in tempSentencesArray)
            {
                List<string> foundWords = ListWithAllWordsInString(currentSentence);
                if (foundWords.Count > 0)
                    sentencesList.Add(foundWords);
            }
            return sentencesList;
        }
    }
} 
