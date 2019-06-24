using System;

namespace Names
{
    internal static class HeatmapTask
    {
		public static string[] FillArray(int length, int startValue)
		{
			var label = new string[length];
            for (int i = 0; i < length; i++)
                label[i] = (i + startValue).ToString();
			return label;
		}
		
        public static HeatmapData GetBirthsPerDateHeatmap(NameData[] names)
        {
            var amountOfDays = 30;
            var amountOfMonths = 12;			
            var birthsCounts = new double[amountOfDays, amountOfMonths];
            foreach (var specificPerson in names)
                if (specificPerson.BirthDate.Day != 1)
                    birthsCounts[specificPerson.BirthDate.Day - 2, specificPerson.BirthDate.Month - 1]++;
            return new HeatmapData("Тепловая карта рождаемости", birthsCounts, 
								   FillArray(amountOfDays, 2), FillArray(amountOfMonths, 1));
        }
    }
}
