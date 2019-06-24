using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
			while (right - left > 1)
            {
                var middle = left + (right - left) / 2;
				bool phraseStartsWithPrefix = phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
				bool phraseIsNotBiggerThanPrefix = string.Compare(prefix, phrases[middle], StringComparison.OrdinalIgnoreCase) > 0;
                if (phraseIsNotBiggerThanPrefix || phraseStartsWithPrefix)
                    left = middle;
                else right = middle;
            }            
            return right;
        }
    }
}
