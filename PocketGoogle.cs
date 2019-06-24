using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        static readonly char[] Splitters = { ' ', '.', ',', '!', '?', ':', '-', '\r', '\n' };
        Dictionary<string, Dictionary<int, List<int>>> documentWords = 
								 new Dictionary<string, Dictionary<int, List<int>>>();
        Dictionary<int, List<string>> documents = new Dictionary<int, List<string>>();

        public void Add(int id, string documentText)
        {
            var words = documentText.Split(Splitters);
            var currentIndex = 0;
            for (int i = 0; i < words.Count(); i++) 
			{
				if (!documentWords.ContainsKey(words[i]))
                	documentWords[words[i]] = new Dictionary<int, List<int>>();
                if (!documentWords[words[i]].ContainsKey(id))
                	documentWords[words[i]][id] = new List<int>();
                documentWords[words[i]][id].Add(currentIndex);
                currentIndex+= words[i].Length + 1;
            }
            if (!documents.ContainsKey(id))
                documents[id] = new List<string>();
            documents[id] = words.ToList();
        }

        public List<int> GetIds(string word)
        {
            if (documentWords.ContainsKey(word))
                return documentWords[word].Keys.ToList();
            return new List<int>();
        }

        public List<int> GetPositions(int id, string word)
        {
            if (documentWords.ContainsKey(word))
                if (documentWords[word].ContainsKey(id))
                    return documentWords[word][id];
            return new List<int>();
        }

        public void Remove(int id)
        {
            if (documents.ContainsKey(id))
            {
                var listIfWords = documents[id];
                foreach (string word in listIfWords)
                    documentWords[word].Remove(id);
				documents.Remove(id);
            }
        }
    }
}
