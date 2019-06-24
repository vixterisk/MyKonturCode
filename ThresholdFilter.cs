using System;
using System.Collections.Generic;

namespace Recognizer
{
    public static class ThresholdFilterTask
    {
        public static double CalculateT(double[,] original, int amountOfWhitePixels)
        {
            var pixelValueOccurrence = new List<double>();
            foreach (double currentPixel in original)
                pixelValueOccurrence.Add(currentPixel);
            pixelValueOccurrence.Sort();
            return pixelValueOccurrence[pixelValueOccurrence.Count - Math.Max(amountOfWhitePixels, 1)];
        }

		public static double[,] ThresholdFilter(double[,] original, double whitePixelsFraction)
		{
            var amountOfWhitePixels = (int)(whitePixelsFraction * original.Length);
            var t = CalculateT(original, amountOfWhitePixels);
            int amountOfRows = original.GetLength(0);
            int amountOfCols = original.GetLength(1);
            var thresholdFilter = new double[amountOfRows, amountOfCols];
            for (int i = 0; i < amountOfRows; i++)
                for (int j = 0; j < amountOfCols; j++)
                    if (original[i, j] >= t && amountOfWhitePixels>0)
                        thresholdFilter[i, j] = 1.0;
                    else
                        thresholdFilter[i, j] = 0.0;
            return thresholdFilter;
		}
	}
}
