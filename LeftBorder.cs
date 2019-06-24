using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class LeftBorderTask
    {
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right - left <= 1) return --right;
            var middle = left + (right - left) / 2;
            if (string.Compare(phrases[middle], prefix, StringComparison.OrdinalIgnoreCase) < 0)
            	return GetLeftBorderIndex(phrases, prefix, middle, right);
			return GetLeftBorderIndex(phrases, prefix, left, middle);
        }
    }
}
