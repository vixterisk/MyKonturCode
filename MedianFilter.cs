using System;
using System.Collections.Generic;
using System.Linq;

namespace Recognizer
{
    internal static class MedianFilterTask
    {
        public static List<double> LocalityValues(double[,] original, int rowIndex, int colIndex)
        {
            List<double> localityValues = new List<double>();
            var amountOfRows = original.GetLength(0);
            var amountOfCols = original.GetLength(1);
            var initialI = rowIndex - 1 < 0 ? 0 : -1;
            var initialJ = colIndex - 1 < 0 ? 0 : -1;
            var lastI = Math.Min(amountOfRows, 2);
            var lastJ = Math.Min(amountOfCols, 2);
            for (int i = initialI; i < lastI && rowIndex + i < amountOfRows; i++)
                for (int j = initialJ; j < lastJ && colIndex + j < amountOfCols; j++)
                    localityValues.Add(original[rowIndex + i, colIndex + j]);
            return localityValues;
        }

        public static double MedianPixel(double[,] original, int rowIndex, int colIndex)
        {
            List<double> localityValues;
            localityValues = LocalityValues(original, rowIndex, colIndex);
            localityValues.Sort();
            double medElement = (double)(localityValues.Count - 1) / 2;
            if (localityValues.Count % 2 == 0)
            {
                int lowerIndex = (int)Math.Floor(medElement);
                int upperIndex = (int)Math.Ceiling(medElement);
                return (localityValues[lowerIndex] + localityValues[upperIndex]) / 2;
            }
            return localityValues[(int)medElement];
        }

        public static double[,] MedianFilter(double[,] original)
        {
            int amountOfRows = original.GetLength(0);
            int amountOfCols = original.GetLength(1);
            double[,] medianFilter = new double[amountOfRows, amountOfCols];
            for (int i = 0; i < amountOfRows; i++)
                for (int j = 0; j < amountOfCols; j++)
                    medianFilter[i, j] = MedianPixel(original, i, j);
            return medianFilter;
        }
    }
}
