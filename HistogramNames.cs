using System;
using System.Linq;

namespace Names
{
    internal static class HistogramTask
    {
		public static string[] FillLabelArray(int amountOfDays)
		{
			var label = new string[amountOfDays];
            for (int d = 0; d < amountOfDays; d++)
                label[d] = (d+1).ToString();
			return label;
		}
		
        public static HistogramData GetBirthsPerDayHistogram(NameData[] names, string name)
        {
            var amountOfDays = 31;
            var birthsCounts = new double[amountOfDays];
            foreach (var specificPerson in names)
                if (specificPerson.Name == name)
                	birthsCounts[specificPerson.BirthDate.Day - 1]++;
            birthsCounts[0] = 0;
            return new HistogramData(string.Format("Рождаемость людей с именем '{0}'", name), 
									 FillLabelArray(amountOfDays), birthsCounts);
        }
    }
}
