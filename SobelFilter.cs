using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double ResultForCurrentPixel(double[,] g, double[,] sx, int x, int y)
        {
            var filterMatrixSize = sx.GetLength(0);
            var localitySize = filterMatrixSize / 2;
            int minorCurrentLocality = -localitySize;
            double gx = 0, gy = 0;
            for (int i = 0; i < filterMatrixSize; i++)
            {
                int majorCurrrentLocality = -localitySize;
                for (int j = 0; j < filterMatrixSize; j++)
                {
                    gx += sx[i, j] * g[x + majorCurrrentLocality, y + minorCurrentLocality];
                    gy += sx[j, i] * g[x + majorCurrrentLocality, y + minorCurrentLocality];
                    majorCurrrentLocality++;
                }
                minorCurrentLocality++;
            }
            return Math.Sqrt(gx * gx + gy * gy); 
        }

        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var localitySize = sx.GetLength(0) / 2;
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            for (int x = localitySize; x < width - localitySize; x++)
                for (int y = localitySize; y < height - localitySize; y++)
                    result[x, y] = ResultForCurrentPixel(g, sx, x, y);
            return result;
        }
    }
}
