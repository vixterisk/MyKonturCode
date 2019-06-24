using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
 
namespace Autocomplete
{
    internal class AutocompleteTask
    {
        public static string FindFirstByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            if (index < phrases.Count && phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                return phrases[index];
            return null;
        }
 
        public static string[] GetTopByPrefix(IReadOnlyList<string> phrases, string prefix, int count)
        {
            var index = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count) + 1;
            List<string> words = new List<string>();
			for (int i = index; i < phrases.Count && 
				 					phrases[i].StartsWith(prefix, StringComparison.OrdinalIgnoreCase) && 									 words.Count < count; i++)
            {
                words.Add(phrases[i]);
            }
            return words.ToArray();
        }
 
        public static int GetCountByPrefix(IReadOnlyList<string> phrases, string prefix)
        {
            var leftIndex = LeftBorderTask.GetLeftBorderIndex(phrases, prefix, -1, phrases.Count);
            var rightIndex = RightBorderTask.GetRightBorderIndex(phrases, prefix, -1, phrases.Count);
            return rightIndex - leftIndex - 1;
        }
    }
 
    [TestFixture]
    public class AutocompleteTests
    {
        [Test]
        public void TopByPrefix_IsEmpty_WhenEmptyPrefix()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "";
            var count = 2;
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            var expectedResult = new String[] { "aa", "ab" };  
            Assert.AreEqual(expectedResult, actualTopWords);
        }
 
        [Test]
        public void TopByPrefix_IsEmpty_WhenNoPhrases()
        {
            var phrases = new List <string>();
            var count = 2;
            var prefix = "ab";
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            CollectionAssert.IsEmpty(actualTopWords);
        }
 
        [Test]
        public void TopByPrefix_IsEmpty_WhenCountIsZero()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "ab";
            var count = 0;
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            CollectionAssert.IsEmpty(actualTopWords);
        }
 
        [Test]
        public void TopByPrefix_IsEmpty_WhenCountIsLargerThanActualTopWords()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "ab";
            var count = 10;
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            var expectedCount = new String[] {"ab", "abb", "abc" };
            Assert.AreEqual(expectedCount, actualTopWords);
        }
 
        [Test]
        public void TopByPrefix_IsEmpty_WhenCountIsNotLargerThanActualTopWords()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "ab";
            var count = 2;
            var actualTopWords = AutocompleteTask.GetTopByPrefix(phrases, prefix, count);
            var expectedCount = new String[] { "ab", "abb" };
            Assert.AreEqual(expectedCount, actualTopWords);
        }
 
        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPrefix()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "";
            var actualTopWords = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = phrases.Count;
            Assert.AreEqual(expectedCount, actualTopWords);
        }
 
        [Test]
        public void CountByPrefix_IsTotalCount_WhenEmptyPhrases()
        {
            var phrases = new List<string>() { };
            var prefix = "ab";
            var actualTopWords = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, actualTopWords);
        }
 
        [Test]
        public void CountByPrefix_IsTotalCount()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "ab";
            var actualTopWords = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 3;
            Assert.AreEqual(expectedCount, actualTopWords);
        }
 
        [Test]
        public void CountByPrefix_IsTotalCount_NoPhrasesThatStartsWithPrefix()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa" };
            var prefix = "cb";
            var actualTopWords = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 0;
            Assert.AreEqual(expectedCount, actualTopWords);
        }
		
        public void CountByPrefix_IsTotalCount_PrefixIsInTheMiddleOfRow()
        {
            var phrases = new List<string>() { "a", "aa", "ab", "abb", "abc", "acc" };
            var prefix = "ab";
            var actualTopWords = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualTopWords);
        }
		
		public void CountByPrefix_IsTotalCount_PrefixIsInTheEndOfRow()
        {
            var phrases = new List<string>() { "aa", "ab", "abb", "abc", "ac", "csa", "css"};
            var prefix = "cs";
            var actualTopWords = AutocompleteTask.GetCountByPrefix(phrases, prefix);
            var expectedCount = 3;
            Assert.AreEqual(expectedCount, actualTopWords);
        }
    }
}
