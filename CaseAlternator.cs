public class CaseAlternatorTask
{
	public static List<string> AlternateCharCases(string lowercaseWord)
    {
    	var result = new List<string>();
        AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
        return result;
    }
	
    static void AlternateCharCases(char[] word, int position, List<string> result)
    {
        if (position == word.Length)
        {
			string newWord = new string(word);
        	result.Add(newWord);
        	return;
        }
        char lowerSymbol = Char.ToLower(word[position]);
        char upperSymbol = Char.ToUpper(word[position]);
        if (lowerSymbol != upperSymbol && Char.IsLetter(lowerSymbol))
        {
        	word[position] = lowerSymbol;
        	AlternateCharCases(word, position + 1, result);
       		word[position] = upperSymbol;
        	AlternateCharCases(word, position + 1, result);
        }
       	else AlternateCharCases(word, position + 1, result);
    }
}
