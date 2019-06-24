using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TextAnalysis
{
    static class FrequencyAnalysisTask
    {
        public static Dictionary<string, int> FindNgramsInText(List<List<string>> text, int n)
        {
            var nrigrams = new Dictionary<string, int>();
            foreach (var currentSentence in text)
            {
                var currentNgram = new StringBuilder();
                for (int i = 0; i < currentSentence.Count - n + 1; i++)
                {
                    for (int j = i; j < i + n - 1; j++)
                        currentNgram.Append(currentSentence[j] + " ");
                    currentNgram.Append(currentSentence[i + n - 1]);
					string currentNgramInStringForm = currentNgram.ToString();
                    if (!nrigrams.ContainsKey(currentNgramInStringForm)) nrigrams[currentNgramInStringForm] = 0;
                    nrigrams[currentNgramInStringForm]++;
                    currentNgram.Clear();
                }
            }
            return nrigrams;
        }

        public static Dictionary<string, string> FormFrequencyDictionary(Dictionary<string, int> ngrams, int n)
        {
            var frequencyDictionary = new Dictionary<string, string>();
            foreach (var currentNgram in ngrams)
            {
                var ngramWords = currentNgram.Key.ToString().Split(' ');
                var firstWord = new StringBuilder();
                for (int j = 0; j < n - 2; j++)
                    firstWord.Append(ngramWords[j] + " ");
                firstWord.Append(ngramWords[n - 2]);
                var currentKey = firstWord.ToString();
                if (!frequencyDictionary.ContainsKey(currentKey)) frequencyDictionary[currentKey] = ngramWords[n - 1];
                else
                    if (ShouldWeReplace(ngrams, frequencyDictionary, currentKey, ngramWords[n - 1]))
                        frequencyDictionary[currentKey] = ngramWords[n - 1];
                firstWord.Clear();
            }
            return frequencyDictionary;
        }

        public static bool ShouldWeReplace(Dictionary<string, int> bigrams, 
										   Dictionary<string, string> frequencyDictionary, 
										   string currentKey, string newValue)
        {
            var frequenceDifference = bigrams[currentKey + " " + frequencyDictionary[currentKey]];
			frequenceDifference -= bigrams[currentKey + " " + newValue];
            return frequenceDifference < 0 || 
				   frequenceDifference == 0 && 
				string.CompareOrdinal(newValue, frequencyDictionary[currentKey]) < 0;
        }

        public static Dictionary<string, string> UniteDictionaries(Dictionary<string, string> oldDict1, Dictionary<string, string> oldDict2)
        {
            var dictionariesUnited = new Dictionary<string, string>();
            CopyDictionary(oldDict1, dictionariesUnited);
            CopyDictionary(oldDict2, dictionariesUnited);
            return dictionariesUnited;
        }

        public static void CopyDictionary(Dictionary<string, string> oldDict, Dictionary<string, string> newDict)
        {            
            foreach (var currentRecord in oldDict)
                newDict[currentRecord.Key] = currentRecord.Value;
        }

        public static Dictionary<string, string> GetMostFrequentNextWords(List<List<string>> text)
        {
            var result = new Dictionary<string, string>();
            var birgams = FindNgramsInText(text, 2);
            var trirgams = FindNgramsInText(text, 3);
            var bigramsFrequence = FormFrequencyDictionary(birgams, 2);
            var trigramsFrequence = FormFrequencyDictionary(trirgams, 3);
            result = UniteDictionaries(bigramsFrequence, trigramsFrequence);
            return result;
        }
    }
}
