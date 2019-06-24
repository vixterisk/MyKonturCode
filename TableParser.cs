using System.Collections.Generic;
using System.Text;

namespace TableParser
{
	public class FieldsParserTask
	{
		public static List<string> ParseLine(string line)
		{
            var listOfTokens = new List<string>();
            for (int i = 0; i < line.Length;)
            {
                var currentToken = ReadField(line, i);
                if (currentToken != null)
                {
                    listOfTokens.Add(currentToken.Value);
                    i = currentToken.GetIndexNextToToken();
                }
                else i++;
            }
			return listOfTokens;
		}

		public static Token ReadField(string line, int givenStartIndex)
		{            
            int startIndex = givenStartIndex;
            while (startIndex < line.Length && line[startIndex] == ' ')
                startIndex++;
            int currentIndex = startIndex;
            if (currentIndex == line.Length)
                return null;
            if (line[currentIndex] == '"')
                return QuotesField(line, currentIndex, '"');
            if (line[currentIndex] == '\'')
                return QuotesField(line, currentIndex, '\'');
            return SimpleField(line, currentIndex);
        }

        private static Token SimpleField(string line, int startIndex)
        {
            var newToken = new StringBuilder();
            int currentIndex = startIndex;
            while (currentIndex < line.Length && line[currentIndex] != '\"' && line[currentIndex] != '\'' && line[currentIndex] != ' ')
            {
                newToken.Append(line[currentIndex]);
                currentIndex++;
            }
            return new Token(newToken.ToString(), startIndex, newToken.Length);
        }

        private static Token QuotesField(string line, int startIndex, char typeOfQuotes)
        {
            var newToken = new StringBuilder();
            int currentIndex = startIndex;
            currentIndex++;
            while (currentIndex < line.Length && line[currentIndex] != typeOfQuotes)
            {
                if (line[currentIndex] == '\\')
                    currentIndex++;
                newToken.Append(line[currentIndex]);
                currentIndex++;
            }
            return new Token(newToken.ToString(), startIndex, currentIndex + 1 - startIndex);
        }
    }
}
