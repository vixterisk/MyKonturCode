using System.Collections.Generic;
using System.Text;

namespace TextAnalysis
{
    static class TextGeneratorTask
    {
        public static string ContainsLastWords(Dictionary<string, string> dictionary, string secondLastWord, string lastWord)
        {
            if (secondLastWord != null && dictionary.ContainsKey(secondLastWord + " " + lastWord))
                return dictionary[secondLastWord + " " + lastWord];
            if (lastWord != null && dictionary.ContainsKey(lastWord))
                return dictionary[lastWord];
            return null;
        }

        public static string ContinuePhrase(Dictionary<string, string> nextWords, string phraseBeginning, int wordsCount)
        {
            var phrase = new StringBuilder();
            phrase.Append(phraseBeginning);
            var wordsInPhrase = phraseBeginning.Split();
            int amountOfWordsInPhrase = wordsInPhrase.Length;
            string secondLastWord;
            if (amountOfWordsInPhrase < 2)
                secondLastWord = null;
            else secondLastWord = wordsInPhrase[amountOfWordsInPhrase - 2];
            string lastWord = wordsInPhrase[amountOfWordsInPhrase - 1];
            for (int i = 0; i < wordsCount; i++)
            {
                string continuation = ContainsLastWords(nextWords, secondLastWord, lastWord);
                if (continuation != null)
					{
            			phrase.Append(" " + continuation);
            			secondLastWord = lastWord;
            			lastWord = continuation;
        			}
                else break;
            }
            return phrase.ToString();
        }
    }
}
